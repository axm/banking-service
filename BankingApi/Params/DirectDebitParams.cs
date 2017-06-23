using System;
using Accounts.Domain;

namespace BankingApi.Params
{
    public class DirectDebitParams
    {
        public Guid FromAccountId { get; set; }
        public decimal Amount { get; set; }
        public Guid ToAccountId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DirectDebitFrequency Frequency { get; set; }
    }
}
