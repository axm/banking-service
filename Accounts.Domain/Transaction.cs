using Banking.Domain;
using Base.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    [DataContract]
    public class NewTransaction
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

        public NewTransaction(TransactionGuid id, AccountGuid inputAccountId, AccountGuid outputAccountId, DateTimeOffset timestamp, Money balance, Money amount)
        {
            Id = id;
            InputAccountId = inputAccountId;
            OutputAccountId = outputAccountId;
            Timestamp = timestamp;
            Balance = balance.Amount;
            Amount = amount.Amount;
        }
    }

    public abstract class Transaction
    {
        public readonly Money Amount;
        public readonly DateTimeOffset Timestamp;

        public Transaction(Money amount, DateTimeOffset timestamp)
        {
        }
    }

    public class Withdrawal : Transaction
    {
        public readonly AccountGuid FromAccountId;

        public Withdrawal(AccountGuid from, Money amount, DateTimeOffset timestamp) : base(amount, timestamp)
        {
            FromAccountId = from;
        }
    }

    public class Deposit : Transaction
    {
        public readonly AccountGuid ToAccountId;

        public Deposit(AccountGuid to, Money amount, DateTimeOffset timestamp) : base(amount, timestamp)
        {
            ToAccountId = to;
        }
    }

    public class Transfer : Transaction
    {
        public readonly AccountGuid FromAccountId;
        public readonly AccountGuid ToAccountId;

        public Transfer(AccountGuid from, AccountGuid to, Money amount, DateTimeOffset timestamp) : base(amount, timestamp)
        {
            FromAccountId = from;
            ToAccountId = to;
        }
    }
}
