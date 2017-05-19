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
        public readonly Money Balance;

        public AccountData(AccountGuid id, Money balance)
        {
            Id = id;
            Balance = balance;
        }

        public AccountData Deposit(Money money)
        {
            return new AccountData(Id, Balance + money);
        }

        public AccountData Withdraw(Money money)
        {
            if(money > Balance)
            {
                throw new NotEnoughFundsException();
            }

            return new AccountData(Id, Balance - money);
        }
    }
}
