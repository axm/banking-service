using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using CustomerActor.Interfaces;
using Customer.Domain;

namespace CustomerActor
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
    internal class CustomerActor : Actor, ICustomerActor
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Initializes a new instance of CustomerActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public CustomerActor(ActorService actorService, ActorId actorId, ICustomerRepository repository)
            : base(actorService, actorId)
        {
            _repository = repository;
        }

        public async Task PutCustomer(Customer.Domain.Customer customer)
        {
            await _repository.PutCustomer(customer);
        }
    }
}
