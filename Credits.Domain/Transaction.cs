using Base.Types;
using System;

namespace Credits.Domain
{
    public class Transaction
    {
        public TransactionGuid TransactionId { get; set; }
        public CreditAccountGuid CreditAccountId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Money Amount { get; set; }
    }
}
