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
        private AccountData _accountData;
        private readonly IAccountRepository _repository;

        /// <summary>
        /// Initializes a new instance of AccountActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public AccountActor(ActorService actorService, ActorId actorId, IAccountRepository repository)
            : base(actorService, actorId)
        {
            _id = new AccountGuid(actorId.GetGuidId());
            _repository = repository;
        }

        public async Task Deposit(decimal money)
        {
            await LoadIfNecessary();

            await _repository.Deposit(_id, money);
            _accountData = _accountData.Deposit(money);
        }

        public async Task<bool> Withdraw(decimal money)
        {
            await LoadIfNecessary();

            if(await _repository.Withdraw(_id, money))
            { 
                _accountData = _accountData.Withdraw(money);
                return true;
            }

            return false;
        }

        private async Task LoadIfNecessary()
        {
            if(_accountData == null)
            {
                try
                {
                    await Load();
                }
                catch (Exception e)
                {
                    ActorEventSource.Current.Message("Exception: " + e.Message);
                    throw;
                }
            }
        }

        private async Task Load()
        {
            _accountData = await _repository.Get(_id);
        }

        public async Task Transfer(Guid to, decimal amount)
        {
            await LoadIfNecessary();

            await _repository.Transfer(_accountData.Id, new AccountGuid(to), amount);
        }

        public async Task SetOverdraft(decimal money)
        {
            await LoadIfNecessary();

            throw new NotImplementedException();
        }

        public async Task VerifyIntegrity()
        {
        }

        public async Task SetUpDirectDebit(DirectDebit directDebit)
        {
        }
    }
}
