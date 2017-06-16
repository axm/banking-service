using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Domain;
using Accounts.Domain;

namespace BankingApi.Params
{
    public class DirectDebitParams
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public Guid ToAccountId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DirectDebitFrequency Frequency { get; set; }
    }
}
