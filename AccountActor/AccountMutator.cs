using Accounts.Domain;
using Banking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountActor
{
    public class AccountMutator
    {
        private readonly AccountData _accountData;

        public AccountMutator(AccountData accountData)
        {
            _accountData = accountData;
        }

        public AccountData ApplyTransaction(NewTransaction transaction)
        {
            if(transaction.InputAccountId == _accountData.Id)
            {
                // _accountData.Balance -= transaction.Amount;
            }
            else
            {
                // _accountData.Balance += transaction.Amount;
            }

            _accountData.Transactions.Add(transaction);

            return _accountData;
        }
    }
}
