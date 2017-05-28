using System;

namespace BankingApi.Controllers
{
    public class MakeTransactionParams
    {
        public Guid CreditAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}