﻿using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using AccountActor.Interfaces;
using Accounts.Domain;
using Base.Types;
using Base.Providers;
using Accounts.Entities;
using System.Collections.Generic;

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
        private readonly IAccountRepository _repository;
        private readonly IDateTimeProvider _dateTimeService;
        private AccountMutator AccountMutator;
        private readonly IServiceBusProvider _serviceBusProvider;
        private readonly IElasticSearchProvider _elasticSearchProvider;

        public AccountActor(ActorService actorService, ActorId actorId, IAccountRepository repository, IDateTimeProvider dateTimeService, IServiceBusProvider serviceBusProvider, IElasticSearchProvider elasticSearchProvider) : base(actorService, actorId)
        {
            _id = new AccountGuid(actorId.GetGuidId());
            _repository = repository;
            _dateTimeService = dateTimeService;
            _serviceBusProvider = serviceBusProvider;
            _elasticSearchProvider = elasticSearchProvider;
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
            AccountData = new AccountData(AccountData.Id, AccountData.SortCode, AccountData.Overdraft, 0);
            AccountMutator = new AccountMutator(AccountData);

            var transactions = await _repository.GetTransactions(_id, null);

            foreach (var transaction in transactions)
            {
                AccountMutator.ApplyTransaction(transaction);
            }
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

            var today = _dateTimeService.GetDateTimeOffset().Date;
            var transactions = AccountData.Transactions
                .Where(t => t.Timestamp.Date >= today.AddMonths(-1).Date)
                .GroupBy(t => t.Timestamp.Date)
                .ToDictionary(t => t.Key, t => t.AsEnumerable().OrderByDescending(_ => _.Timestamp).First());
            var days = (today - today.AddMonths(-1)).Days;

        }

        public async Task MakeTransaction(AccountGuid toAccountId, DateTimeOffset timestamp, Money amount)
        {
            await LoadIfNecessary();
            var transaction = new Transaction(new TransactionGuid(), _id, toAccountId, timestamp, AccountData.Balance, amount);

            await _repository.StoreTransaction(transaction);

            AccountMutator.ApplyTransaction(transaction);
        }
    }
}
