using System;
using Accounts.Domain;

namespace BankingApi.Controllers
{
    public class MakePaymentParams
    {
        public Guid CreditAccountId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}