using Base.Types;
using System;

namespace Accounts.Entities
{
    public class DirectDebit
    {
        public DirectDebitGuid Id { get; set; }
        public Money Amount { get; set; }
        public AccountGuid FromAccountId { get; set; }
        public AccountGuid ToAccountId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? LastRunTimestamp { get; set; }
        public DirectDebitFrequency Frequency { get; set; }
    }
}