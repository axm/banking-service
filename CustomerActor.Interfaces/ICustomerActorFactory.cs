using Customer.Domain;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerActor.Interfaces
{
    public interface ICustomerActorFactory
    {
        ICustomerActor Create(CustomerGuid customerId);
    }

    public class CustomerActorFactory : ICustomerActorFactory
    {
        public ICustomerActor Create(CustomerGuid customerId)
        {
            return ActorProxy.Create<ICustomerActor>(new ActorId(customerId), new Uri("fabric:/BankingService/CustomerActorService"));
        }
    }
}
