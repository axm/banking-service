using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Configuration;
using MongoDB.Driver;
using Base.Types;
using AccountActor.Interfaces;
using MongoDB.Bson.Serialization;
using Base.Serialization;
using Base.Providers;

namespace DirectDebitService
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
                var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                var mongoConnectionString = ConfigurationManager.ConnectionStrings["MongoDefault"].ConnectionString;
                var elasticSearchConnection = ConfigurationManager.ConnectionStrings["ElasticSearchDefault"].ConnectionString;
                var mongoClient = new MongoClient();
                var dateTimeService = new DateTimeService();
                var directDebitRepository = new DirectDebitRepository(connectionString, dateTimeService, mongoClient);

                var elasticSearchProvider = new ElasticSearchProvider(elasticSearchConnection);

                ServiceRuntime.RegisterServiceAsync("DirectDebitServiceType",
                    context => new DirectDebitService(context, directDebitRepository, new AccountActorFactory(), dateTimeService, elasticSearchProvider)).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(DirectDebitService).Name);


                BsonSerializer.RegisterSerializer(typeof(DirectDebitGuid), new DirectDebitGuidSerializer());
                BsonSerializer.RegisterSerializer(typeof(AccountGuid), new AccountGuidSerializer());
                BsonSerializer.RegisterSerializer(typeof(DateTimeOffset), new BankingDateTimeOffsetSerializer());
                BsonSerializer.RegisterSerializer(typeof(Money), new MoneySerializer());

                // Prevents this host process from terminating so services keep running.
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
