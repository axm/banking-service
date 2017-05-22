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
        private readonly ICreditRepository _repository;

        public CreditAccountActor(ActorService actorService, ActorId actorId, ICreditRepository repository)
            : base(actorService, actorId)
        {
            _repository = repository;
        }

        public async Task MakePayment(PaymentParams paymentParams)
        {
            var payment = new Payment
            {
                Amount = paymentParams.Amount
            };

            await _repository.MakePayment(payment);
        }

        public async Task MakeTransaction(TransactionParams transactionParams)
        {
            var transaction = new Transaction
            {
                Amount = transactionParams.Amount
            };

            await _repository.MakeTransaction(transaction);
        }
    }
}
