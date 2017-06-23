using System;
using System.Threading;
using Microsoft.ServiceFabric.Actors.Runtime;
using AccountActor.Interfaces;
using System.Configuration;
using Base.Types;
using MongoDB.Bson.Serialization;
using Base.Providers;
using Base.Serialization;

namespace AccountActor
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

                var repository = new AccountRepository(ConfigurationManager.ConnectionStrings["Default"].ConnectionString, ConfigurationManager.ConnectionStrings["MongoDefault"].ConnectionString);
                var elasticSearchConnection = ConfigurationManager.ConnectionStrings["ElasticSearchDefault"].ConnectionString;
                var dateTimeService = new DateTimeService();

                BsonSerializer.RegisterSerializer(typeof(DirectDebitGuid), new DirectDebitGuidSerializer());
                BsonSerializer.RegisterSerializer(typeof(AccountGuid), new AccountGuidSerializer());
                BsonSerializer.RegisterSerializer(typeof(DateTimeOffset), new BankingDateTimeOffsetSerializer());
                BsonSerializer.RegisterSerializer(typeof(Money), new MoneySerializer());

                repository.CreateAccountStore();

                var serviceBusProvider = new ServiceBusProvider();
                var elasticSearchProvider = new ElasticSearchProvider(elasticSearchConnection);

                ActorRuntime.RegisterActorAsync<AccountActor>(
                   (context, actorType) => new ActorService(context, actorType, (svc, id) => new AccountActor(svc, id, repository, dateTimeService, serviceBusProvider, elasticSearchProvider))).GetAwaiter().GetResult();

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
