using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credits.Domain
{
    public class Payment
    {
        public PaymentGuid PaymentId { get; set; }
        public CreditAccountGuid CreditAccountId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public decimal Amount { get; set; }
    }
}
