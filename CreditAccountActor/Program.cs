using System;
using System.Threading;
using Microsoft.ServiceFabric.Actors.Runtime;
using CreditAccountActor.Repository;
using System.Configuration;
using Base.Types;
using Base.Providers;

namespace CreditAccountActor
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // This line registers an Actor Service to host your actor class with the Service Fabric runtime.
                // The contents of your ServiceManifest.xml and ApplicationManifest.xml files
                // are automatically populated when you build this project.
                // For more information, see https://aka.ms/servicefabricactorsplatform

                var repository = new CreditRepository(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
                var mongoConnection = new CreditRepository(ConfigurationManager.ConnectionStrings["MongoDefault"].ConnectionString);
                var elasticSearchConnection = ConfigurationManager.ConnectionStrings["ElasticSearchDefault"].ConnectionString;
                var dateTimeService = new DateTimeService();

                var elasticSearchProvider = new ElasticSearchProvider(elasticSearchConnection);

                ActorRuntime.RegisterActorAsync<CreditAccountActor>(
                   (context, actorType) => new ActorService(context, actorType, (svc, id) => new CreditAccountActor(svc, id, repository, dateTimeService, elasticSearchProvider))).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
