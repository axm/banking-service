using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;
using CreditAccountActor.Repository;
using System.Configuration;
using Common.Services;

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
                var dateTimeService = new DateTimeService();

                ActorRuntime.RegisterActorAsync<CreditAccountActor>(
                   (context, actorType) => new ActorService(context, actorType, (svc, id) => new CreditAccountActor(svc, id, repository, dateTimeService))).GetAwaiter().GetResult();

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
