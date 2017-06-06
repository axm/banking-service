using Banking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    public class AccountData
    {
        public readonly AccountGuid Id;
        public readonly SortCode SortCode;
        public readonly Money Overdraft;
        public readonly Money Balance;

        public AccountData(AccountGuid id, SortCode sortCode, Money overdraft, Money balance)
        {
            Id = id;
            SortCode =  sortCode;
            Overdraft = overdraft;
            Balance = balance;
        }

        public AccountData Deposit(Money money)
        {
            return new AccountData(Id, SortCode, Overdraft, Balance + money);
        }

        public AccountData Withdraw(Money money)
        {
            if(money > Balance + Overdraft)
            {
                throw new NotEnoughFundsException();
            }

            return new AccountData(Id, SortCode, Overdraft, Balance - money);
        }
    }
}
