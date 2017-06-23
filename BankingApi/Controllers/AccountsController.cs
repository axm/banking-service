using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccountActor.Interfaces;
using Accounts.Domain;
using BankingApi.Params;
using Banking.Domain;
using System.Diagnostics;

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        // GET api/values
        private readonly IAccountActorFactory _accountActorFactory;

        public AccountsController(IAccountActorFactory accountActorFactory)
        {
            _accountActorFactory = accountActorFactory;
        }

        [HttpPost("Withdraw")]
        public async Task Withdraw([FromBody]WithdrawParams withdrawParams)
        {
            var actor = _accountActorFactory.Create(new AccountGuid(withdrawParams.AccountId));

            await actor.MakeTransaction(null, DateTimeOffset.Now, new Money(withdrawParams.Amount));
        }

        [HttpPost("Deposit")]
        public async Task Deposit([FromBody]DepositParams depositParams)
        {
            var actor = _accountActorFactory.Create(new AccountGuid(depositParams.AccountId));

            await actor.MakeTransaction(new AccountGuid(depositParams.AccountId), DateTimeOffset.Now, new Money(depositParams.Money));
        }

        [HttpPost("Transfer")]
        public async Task Transfer([FromBody]TransferParams transferParams)
        {
            var actor = _accountActorFactory.Create(new AccountGuid(transferParams.From));

            await actor.MakeTransaction(new AccountGuid(transferParams.To), DateTimeOffset.Now, new Money(transferParams.Amount));
        }

        [HttpPost("Transactions")]
        public async Task Transactions([FromBody]TransactionsParams transactionsParams)
        {
            var account = transactionsParams.InputAccountId != null ? new AccountGuid(transactionsParams.InputAccountId) : new AccountGuid(transactionsParams.OutputAccountId);

            var actor = _accountActorFactory.Create(account);
            await actor.MakeTransaction(transactionsParams.OutputAccountId != null ? new AccountGuid(transactionsParams.OutputAccountId) : (AccountGuid)null,
                DateTimeOffset.Now,
                transactionsParams.Amount);
        }

        [HttpPost("DirectDebit")]
        public async Task DirectDebit([FromBody]DirectDebitParams directDebitParams)
        {
            var accountId = new AccountGuid(directDebitParams.FromAccountId);
            var toAccountId = new AccountGuid(directDebitParams.ToAccountId);

            var actor = _accountActorFactory.Create(accountId);
            await actor.PostDirectDebit(directDebitParams.Amount, toAccountId, directDebitParams.StartTime, directDebitParams.Frequency);
        }

        [HttpDelete("DirectDebit")]
        public async Task DirectDebit([FromBody]DeleteDirectDebitParams deleteDirectDebitParams)
        {
            var directDebitId = new DirectDebitGuid(deleteDirectDebitParams.DirectDebitId);
            var accountId = new AccountGuid(deleteDirectDebitParams.AccountId);

            var actor = _accountActorFactory.Create(accountId);
            await actor.DeleteDirectDebit(directDebitId);
        }
    }
}
