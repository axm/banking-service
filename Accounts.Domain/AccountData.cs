using Accounts.Entities;
using Base.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Accounts.Domain
{
    public class AccountData
    {
        public AccountGuid Id { get; private set; }
        public SortCode SortCode { get; private set; }
        public Money Overdraft { get; private set; }
        public Money Balance { get; private set; }
        public IReadOnlyCollection<Transaction> Transactions => new ReadOnlyCollection<Transaction>(_transactions);
        private readonly List<Transaction> _transactions;

        public AccountData(AccountGuid id, SortCode sortCode, Money overdraft, Money balance, List<Transaction> transactions)
        {
            Id = id;
            SortCode =  sortCode;
            Overdraft = overdraft;
            Balance = balance;
            _transactions = transactions;
        }

        public AccountData(AccountGuid id, SortCode sortCode, Money overdraft, Money balance) 
            : this(id, sortCode, overdraft, balance, new List<Transaction>()) { }

        public AccountData(AccountGuid id, SortCode sortCode)
            : this(id, sortCode, 0, 0) { }

        public void AddTransaction(Transaction transaction)
        {
            if(transaction.InputAccountId == Id)
            {
                Balance = new Money(Balance.Amount - transaction.Amount);
            }
            else
            {
                Balance = new Money(Balance.Amount + transaction.Amount);
            }

            _transactions.Add(transaction);
        }
    }
}
