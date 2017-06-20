using Banking.Domain;
using Base.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    public class AccountData
    {
        public AccountGuid Id { get; private set; }
        public SortCode SortCode { get; private set; }
        public Money Overdraft { get; private set; }
        public Money Balance { get; private set; }
        private ICollection<Transaction> Transactions { get; set; }

        public AccountData(AccountGuid id, SortCode sortCode, Money overdraft, Money balance, ICollection<Transaction> transactions)
        {
            Id = id;
            SortCode =  sortCode;
            Overdraft = overdraft;
            Balance = balance;
            Transactions = transactions;
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

            Transactions.Add(transaction);
        }
    }
}
