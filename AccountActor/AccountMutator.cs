using Accounts.Domain;

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
