using Base.Types;
using System;

namespace Credits.Domain
{
    public class Payment
    {
        public PaymentGuid PaymentId { get; set; }
        public AccountGuid FromAccountId { get; set; }
        public CreditAccountGuid CreditAccountId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Money Amount { get; set; }
    }
}
