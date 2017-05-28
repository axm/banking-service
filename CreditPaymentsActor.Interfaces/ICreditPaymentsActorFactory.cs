using Credits.Domain;
using Microsoft.ServiceFabric.Actors.Client;
using System;

namespace CreditPaymentsActor.Interfaces
{
    public interface ICreditPaymentsActorFactory
    {
        ICreditPaymentsActor Create(CreditAccountGuid creditAccountId);
    }

    public class CreditPaymentsActorFactory : ICreditPaymentsActorFactory
    {
        public ICreditPaymentsActor Create(CreditAccountGuid creditAccountId)
        {
            return ActorProxy.Create<ICreditPaymentsActor>(new Microsoft.ServiceFabric.Actors.ActorId(creditAccountId.Id), new Uri("fabric:/BankingService/CreditPaymentsActorService"));
        }
    }
}
