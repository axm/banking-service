using Accounts.Domain;
using Accounts.Entities;
using System;

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
            if(transaction == null)
            {
                throw new ArgumentNullException($"{nameof(transaction)} cannot be null.");
            }

            _accountData.AddTransaction(transaction);

            return _accountData;
        }
    }
}
