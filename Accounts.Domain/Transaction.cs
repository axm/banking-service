using Base.Types;
using System;
using System.Runtime.Serialization;

namespace Accounts.Domain
{
    [DataContract]
    public class Transaction
    {
        [DataMember]
        public TransactionGuid Id { get; private set; }
        [DataMember]
        public AccountGuid InputAccountId { get; private set; }
        [DataMember]
        public AccountGuid OutputAccountId { get; private set; }
        [DataMember]
        public DateTimeOffset Timestamp { get; private set; }
        [DataMember]
        public decimal Balance { get; private set; }
        [DataMember]
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
