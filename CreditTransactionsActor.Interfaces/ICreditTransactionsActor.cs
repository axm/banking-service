using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace CreditTransactionsActor.Interfaces
{
    public interface ICreditTransactionsActor : IActor
    {
        Task<IEnumerable<object>> GetTransactions();
        Task<IEnumerable<object>> GetTransactionsIncludingPendingTransactions();
    }
}
