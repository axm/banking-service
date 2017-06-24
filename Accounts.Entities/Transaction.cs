using Base.Types;
using System;

namespace Accounts.Entities
{
    public class Transaction
    {
        public TransactionGuid Id { get; private set; }
        public AccountGuid InputAccountId { get; private set; }
        public AccountGuid OutputAccountId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }

        public Transaction(TransactionGuid id, AccountGuid inputAccountId, AccountGuid outputAccountId, DateTimeOffset timestamp, Money balance, Money amount)
        {
            Id = id;
            InputAccountId = inputAccountId;
            OutputAccountId = outputAccountId;
            Timestamp = timestamp;
            Balance = balance.Amount;
            Amount = amount.Amount;
        }
    }
}
