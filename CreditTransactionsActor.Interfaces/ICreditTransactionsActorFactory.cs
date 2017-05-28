using Credits.Domain;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;

namespace CreditTransactionsActor.Interfaces
{
    public interface ICreditTransactionsActorFactory
    {
        ICreditTransactionsActor Create(CreditAccountGuid creditAccountId);
    }

    public class CreditTransactionsActorFactory : ICreditTransactionsActorFactory
    {
        public ICreditTransactionsActor Create(CreditAccountGuid creditAccountId)
        {
            return ActorProxy.Create<ICreditTransactionsActor>(new ActorId(creditAccountId.Id), new Uri("fabric:/BankingService/CreditTransactionsActorService"));
        }
    }
}
