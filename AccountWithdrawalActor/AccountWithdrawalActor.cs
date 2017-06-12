using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using AccountWithdrawalActor.Interfaces;
using Accounts.Domain;
using Banking.Domain;

namespace AccountWithdrawalActor
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class AccountWithdrawalActor : Actor, IAccountWithdrawalActor
    {
        private readonly IAccountWithdrawalRepository _repository;

        public AccountWithdrawalActor(ActorService actorService, ActorId actorId, IAccountWithdrawalRepository repository)
            : base(actorService, actorId)
        {
            _repository = repository;
        }

        public async Task Withdraw(AccountGuid accountId, Money money)
        {
            await _repository.Withdraw(accountId, money);
        }
    }
}
