using Base.Types;
using Microsoft.ServiceFabric.Actors.Client;
using System;

namespace CreditAccountActor.Interfaces
{
    public interface ICreditAccountActorFactory
    {
        ICreditAccountActor Create(CreditAccountGuid id);
    }

    public class CreditAccountActorFactory : ICreditAccountActorFactory
    {
        public ICreditAccountActor Create(CreditAccountGuid id)
        {
            return ActorProxy.Create<ICreditAccountActor>(new Microsoft.ServiceFabric.Actors.ActorId(id), new Uri("fabric:/BankingService/CreditAccountActorService"));
        }
    }
}
