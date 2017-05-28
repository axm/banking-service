using Banking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
