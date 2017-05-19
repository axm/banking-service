using System;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers
{
    [Route("api/[controller]")]
    [Obsolete("Use BankingApi")]
    public class AccountsController : Controller
    {
        //private readonly IAccountActorFactory _accountActorFactory;

        //public AccountsController(IAccountActorFactory accountActorFactory)
        //{
        //    _accountActorFactory = accountActorFactory;
        //}

        //// GET api/values
        //[HttpGet]
        //public async Task<IEnumerable<string>> Get()
        //{
        //    var actor = _accountActorFactory.Create(new AccountGuid(new Guid("B7DB0236-026E-46FC-A8CF-0FEAEE9D6AD0")));
        //    await actor.Transfer(new Guid("841C6BE5-DD9A-4DBE-AB29-54BDFF5ECD2F"), 999);

        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        //[HttpPost("Withdraw")]
        //public async Task Withdraw([FromBody]WithdrawParams withdrawParams)
        //{
        //    var actor = _accountActorFactory.Create(new AccountGuid(withdrawParams.AccountId));

        //    await actor.Withdraw(withdrawParams.Amount);
        //}

        //[HttpPost("Deposit")]
        //public async Task Deposit([FromBody]DepositParams depositParams)
        //{
        //    var actor = _accountActorFactory.Create(new AccountGuid(depositParams.AccountId));

        //    await actor.Deposit(depositParams.Money);
        //}

        //[HttpPost("Transfer")]
        //public async Task Transfer([FromBody]TransferParams transferParams)
        //{
        //    var actor = _accountActorFactory.Create(new AccountGuid(transferParams.From));
        //    await actor.Transfer(transferParams.To, transferParams.Amount);
        //}
    }
}
