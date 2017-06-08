using Dapper;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Data.SqlClient;

namespace TransactionProcessingService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class TransactionProcessingService : StatelessService
    {
        private readonly string _connectionString;

        public TransactionProcessingService(StatelessServiceContext context, string connectionString)
            : base(context)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync("Credit.spProcessTransactions", commandType: System.Data.CommandType.StoredProcedure);
                }

                await Task.Delay(10000);
            }
        }
    }
}
