using Banking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
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
