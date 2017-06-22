using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Dapper;
using AccountActor.Interfaces;
using Base.Types;

namespace DirectDebitService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class DirectDebitService : StatelessService
    {
        private readonly IDirectDebitRepository _repository;
        private readonly IAccountActorFactory _accountFactory;
        private readonly IDateTimeService _dateTimeService;

        public DirectDebitService(StatelessServiceContext context, IDirectDebitRepository repository, IAccountActorFactory accountFactory, IDateTimeService dateTimeService)
            : base(context)
        {
            _repository = repository;
            _accountFactory = accountFactory;
            _dateTimeService = dateTimeService;
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
                var directDebits = await _repository.GetDirectDebitsForToday();

                foreach (var debit in directDebits)
                {
                    var actor = _accountFactory.Create(debit.FromAccountId);

                    var timestamp = _dateTimeService.GetDateTimeOffset();
                    await actor.MakeTransaction(debit.FromAccountId, debit.ToAccountId, timestamp, debit.Amount);
                    await _repository.MarkDirectDebit(debit.Id, timestamp);

                }

                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(TimeSpan.FromSeconds(5000), cancellationToken);
            }
        }
    }
}
