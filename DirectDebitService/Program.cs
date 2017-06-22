using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Configuration;
using MongoDB.Driver;
using Base.Types;
using AccountActor.Interfaces;
using MongoDB.Bson.Serialization;
using Banking.Domain;
using Base.Serialization;
using MongoDB.Bson.Serialization.Serializers;

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
                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.

                var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                var mongoConnectionString = ConfigurationManager.ConnectionStrings["MongoDefault"].ConnectionString;
                var mongoClient = new MongoClient();
                var dateTimeService = new DateTimeService();
                var directDebitRepository = new DirectDebitRepository(connectionString, dateTimeService, mongoClient);

                ServiceRuntime.RegisterServiceAsync("DirectDebitServiceType",
                    context => new DirectDebitService(context, directDebitRepository, new AccountActorFactory(), dateTimeService)).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(DirectDebitService).Name);

                BsonSerializer.RegisterSerializer(typeof(DateTimeOffset), new DateTimeOffsetSerializer());
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
