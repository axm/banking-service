using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Params
{
    public class TransactionsParams
    {
        public Guid InputAccountId { get; set; }
        public Guid OutputAccountId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public decimal Amount { get; set; }
    }
}
