using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using CreditAccountActor.Interfaces;
using CreditAccountActor.Interfaces.Params;
using Credits.Interfaces;
using Credits.Domain;
using Base.Types;
using Base.Providers;

namespace CreditAccountActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class CreditAccountActor : Actor, ICreditAccountActor
    {
        private CreditAccount CreditAccount { get; set; }
        private readonly CreditAccountGuid CreditAccountId;
        private readonly ICreditRepository _repository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IElasticSearchProvider _elasticSearchProvider;

        public CreditAccountActor(ActorService actorService, ActorId actorId, ICreditRepository repository, IDateTimeService dateTimeService, IElasticSearchProvider elasticSearchProvider)
            : base(actorService, actorId)
        {
            CreditAccountId = new CreditAccountGuid(actorId.GetGuidId());
            _repository = repository;
            _dateTimeService = dateTimeService;
            _elasticSearchProvider = elasticSearchProvider;
        }

        public async Task MakePayment(PaymentParams paymentParams)
        {
            await LoadIfNecessary();

            var payment = new Payment
            {
                Amount = paymentParams.Amount,
                FromAccountId = paymentParams.FromAccountId,
                Timestamp = DateTimeOffset.Now,
                CreditAccountId = paymentParams.Id,
                PaymentId = new PaymentGuid()
            };

            await _repository.MakePayment(payment);

            CreditAccount = new CreditAccount(CreditAccountId, CreditAccount.Limit, CreditAccount.Interest, CreditAccount.Usage - payment.Amount, CreditAccount.AvailableFunds + payment.Amount);
        }

        public async Task MakeTransaction(TransactionParams transactionParams)
        {
            await LoadIfNecessary();

            if(CreditAccount.AvailableFunds < transactionParams.Amount)
            {
                throw new InvalidOperationException();
            }

            var transaction = new Transaction
            {
                TransactionId = new Credits.Domain.TransactionGuid(),
                CreditAccountId = transactionParams.Id,
                Timestamp = DateTimeOffset.Now,
                Amount = transactionParams.Amount
            };

            await _repository.MakeTransaction(transaction);

            CreditAccount = new CreditAccount(CreditAccountId, CreditAccount.Limit, CreditAccount.Interest, CreditAccount.Usage + transaction.Amount, CreditAccount.AvailableFunds - transaction.Amount);
        }

        private async Task LoadIfNecessary()
        {
            if(CreditAccount == null)
            {
                CreditAccount = await _repository.Get(CreditAccountId);
            }
        }

        public async Task VerifyIntegrity()
        {

        }
    }
}
