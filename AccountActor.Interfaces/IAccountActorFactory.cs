using Accounts.Domain;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using Microsoft.ServiceFabric.Actors;
using Base.Types;

namespace AccountActor.Interfaces
{
    public class AccountActorFactory : IAccountActorFactory
    {
        public IAccountActor Create(AccountGuid id)
        {
            return ActorProxy.Create<IAccountActor>(new ActorId(id.Id), new Uri("fabric:/BankingService/AccountActorService"));
        }
    }

    public interface IAccountActorFactory
    {
        IAccountActor Create(AccountGuid id);
    }
}
