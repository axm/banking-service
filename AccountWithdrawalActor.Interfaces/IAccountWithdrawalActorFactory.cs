using Accounts.Domain;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountWithdrawalActor.Interfaces
{
    public class AccountWithdrawalActorFactory : IAccountWithdrawalActorFactory
    {
        public IAccountWithdrawalActor Create(AccountGuid id)
        {
            return ActorProxy.Create<IAccountWithdrawalActor>(new ActorId(id.Id), new Uri("fabric:/BankingService/AccountWithdrawalActorService"));
        }
    }

    public interface IAccountWithdrawalActorFactory
    {
        IAccountWithdrawalActor Create(AccountGuid id);
    }
}
