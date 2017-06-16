using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using AccountActor.Interfaces;
using Banking.Domain;
using Accounts.Domain;
using Common.Services;
using AccountWithdrawalActor.Interfaces;
using Base.Types;

namespace AccountActor
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
    internal class AccountActor : Actor, IAccountActor
    {
        private readonly AccountGuid _id;
        private AccountData AccountData { get; set; }
        private ICollection<NewTransaction> Transactions { get; set; }
        private readonly IAccountRepository _repository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAccountActorFactory _accountActorFactory;
        private readonly IAccountWithdrawalActorFactory _accountWithdrawalActorFactory;
        private AccountMutator AccountMutator;

        /// <summary>
        /// Initializes a new instance of AccountActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public AccountActor(ActorService actorService, ActorId actorId, IAccountRepository repository, IDateTimeService dateTimeService, IAccountWithdrawalActorFactory accountWithdrawalActorFactory)
            : base(actorService, actorId)
        {
            _id = new AccountGuid(actorId.GetGuidId());
            _repository = repository;
            _dateTimeService = dateTimeService;
            _accountWithdrawalActorFactory = accountWithdrawalActorFactory;
        }

        public async Task Deposit(Money money)
        {
            await LoadIfNecessary();

            await _repository.Deposit(_id, money);
            AccountData = AccountData.Deposit(money);
        }

        public async Task<bool> Withdraw(Money money)
        {
            await LoadIfNecessary();

            if(money > AccountData.Balance)
            {
                return false;
            }

            if(await _repository.Withdraw(_id, money))
            { 
                AccountData = AccountData.Withdraw(money);
                return true;
            }

            var withdrawalsActor = _accountWithdrawalActorFactory.Create(_id);
            await withdrawalsActor.Withdraw(_id, money);

            return false;
        }

        private async Task LoadIfNecessary()
        {
            if(AccountData == null)
            {
                await Load();
            }
        }

        private async Task Load()
        {
            AccountData = await _repository.Get(_id);
            AccountMutator = new AccountMutator(AccountData);

            var transactions = await _repository.GetNewTransactions(_id, null);

            foreach (var transaction in transactions)
            {
                Transactions.Add(transaction);

                AccountMutator.ApplyTransaction(transaction);
            }
        }

        public async Task Transfer(AccountGuid to, Money amount)
        {
            await LoadIfNecessary();

            if(AccountData.Balance < amount)
            {
                throw new InvalidOperationException();
            }

            await _repository.Transfer(AccountData.Id, to, amount);

            AccountData = AccountData.Withdraw(new Money(amount));
        }

        public async Task SetOverdraft(Money amount)
        {
            await LoadIfNecessary();

            await _repository.SetOverdraft(AccountData.Id, amount);
        }

        public async Task VerifyIntegrity()
        {
        }

        public async Task PostDirectDebit(Money amount, AccountGuid toAccountId, DateTimeOffset startTime, DirectDebitFrequency frequency)
        {
            var directDebitId = new DirectDebitGuid();

            var directDebit = new DirectDebit
            {
                Id = directDebitId,
                Amount = amount,
                FromAccountId = _id,
                ToAccountId = toAccountId,
                StartDate = startTime,
                LastRunTimestamp = null,
                Frequency = frequency
            };

            await _repository.PostDirectDebit(directDebit);
        }

        public async Task DeleteDirectDebit(DirectDebitGuid directDebitId)
        {
            await _repository.DeleteDirectDebit(directDebitId);
        }

        public async Task<AccountInfo> GetAccountInfo(MonthYear monthYear)
        {
            return null;
        }

        public async Task ApplyInterest()
        {
            await LoadIfNecessary();

            throw new NotImplementedException();
        }

        public async Task ApplyTransaction(AccountGuid inputAccountId, AccountGuid outputAccountId, DateTimeOffset timestamp, Money amount)
        {
            await LoadIfNecessary();
            var transaction = new NewTransaction(new TransactionGuid(), inputAccountId, outputAccountId, timestamp, AccountData.Balance, amount);

            await _repository.StoreTransaction(transaction);

            AccountMutator.ApplyTransaction(transaction);
        }
    }
}
