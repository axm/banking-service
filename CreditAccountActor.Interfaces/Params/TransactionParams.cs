using Credits.Domain;
using System;

namespace CreditAccountActor.Interfaces.Params
{
    public class TransactionParams
    {
        public CreditAccountGuid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}