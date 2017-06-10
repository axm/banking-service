using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccountActor.Interfaces;
using Accounts.Domain;
using BankingApi.Params;
using Banking.Domain;

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

            await actor.Withdraw(new Money(withdrawParams.Amount));
        }

        [HttpPost("Deposit")]
        public async Task Deposit([FromBody]DepositParams depositParams)
        {
            var actor = _accountActorFactory.Create(new AccountGuid(depositParams.AccountId));

            await actor.Deposit(new Money(depositParams.Money));
        }

        [HttpPost("Transfer")]
        public async Task Transfer([FromBody]TransferParams transferParams)
        {
            var actor = _accountActorFactory.Create(new AccountGuid(transferParams.From));
            await actor.Transfer(new AccountGuid(transferParams.To), new Money(transferParams.Amount));
        }
    }
}
