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

        public AccountData ApplyTransaction(Transaction transaction)
        {
            _accountData.AddTransaction(transaction);

            return _accountData;
        }
    }
}
