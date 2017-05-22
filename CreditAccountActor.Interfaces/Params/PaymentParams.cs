using Accounts.Domain;
using Credits.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditAccountActor.Interfaces.Params
{
    public class PaymentParams
    {
        public CreditAccountGuid Id { get; set; }
        public AccountGuid FromAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
