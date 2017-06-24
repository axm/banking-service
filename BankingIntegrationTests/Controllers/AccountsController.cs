using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankingIntegrationTests.Attributes;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using BankingIntegrationTests.Data;
using Accounts.Domain;
using AccountActor.Interfaces;
using Base.Types;
using MongoDB.Driver;

namespace BankingIntegrationTests.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    [IntegrationTestController]
    public class AccountsController : Controller
    {
        private readonly IAccountActorFactory _accountActorFactory;
        private readonly Random random = new Random();

        public AccountsController(IAccountActorFactory accountActorFactory)
        {
            _accountActorFactory = accountActorFactory;
        }

        [BeforeAll]
        public async Task Setup()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            var mongoConnectionString = ConfigurationManager.ConnectionStrings["MongoDefault"].ConnectionString;
            var mongoClient = new MongoClient(mongoConnectionString);
            var accountsCollection = mongoClient.GetDatabase("local").GetCollection<AccountData>("accounts");

            var accounts = await accountsCollection.FindAsync(_ => _.Id == SampleAccountData.Account1.Id.Id || _.Id == SampleAccountData.Account2.Id.Id || _.Id == SampleAccountData.Account3.Id.Id);
            if(accounts.Any())
            {
                return;
            }

            await accountsCollection.InsertOneAsync(new AccountData(SampleAccountData.Account1.Id, SampleAccountData.Account1.SortCode, SampleAccountData.Account1.Overdraft.Amount, SampleAccountData.Account1.Balance.Amount));
            await accountsCollection.InsertOneAsync(new AccountData(SampleAccountData.Account2.Id, SampleAccountData.Account2.SortCode, SampleAccountData.Account2.Overdraft.Amount, SampleAccountData.Account2.Balance.Amount));
            await accountsCollection.InsertOneAsync(new AccountData(SampleAccountData.Account3.Id, SampleAccountData.Account3.SortCode, SampleAccountData.Account3.Overdraft.Amount, SampleAccountData.Account3.Balance.Amount));
        }

        [IntegrationTest]
        [HttpGet("MakeDeposits")]
        public async Task MakeDeposits()
        {
            await MakeDeposits(SampleAccountData.Account1);
            await MakeDeposits(SampleAccountData.Account2);
            await MakeDeposits(SampleAccountData.Account3);
        }

        private async Task MakeDeposits(AccountData account)
        {
            var actor = _accountActorFactory.Create(account.Id);

            for(var i = 0; i < 10; i++)
            {
                var amount = new Money((decimal)random.NextDouble() * 1000);

                try
                {
                    await actor.MakeTransaction(account.Id, DateTimeOffset.Now, amount);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        [IntegrationTest]
        [HttpGet("MakeWithdrawals")]
        public async Task MakeWithdrawals()
        {
            await MakeWithdrawals(SampleAccountData.Account1);
            await MakeWithdrawals(SampleAccountData.Account2);
            await MakeWithdrawals(SampleAccountData.Account3);
        }

        private async Task MakeWithdrawals(AccountData account)
        {
            var actor = _accountActorFactory.Create(account.Id);

            for (var i = 0; i < 10; i++)
            {
                var amount = new Money((decimal)random.NextDouble() * 10);
                try
                {

                    await actor.MakeTransaction(null, DateTimeOffset.Now, amount);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        [IntegrationTest]
        [HttpGet("MakeTransfers")]
        public async Task MakeTransfers()
        {
            await MakeTransfers(SampleAccountData.Account1, SampleAccountData.Account2);
            await MakeTransfers(SampleAccountData.Account1, SampleAccountData.Account3);
            await MakeTransfers(SampleAccountData.Account2, SampleAccountData.Account1);
            await MakeTransfers(SampleAccountData.Account2, SampleAccountData.Account3);
            await MakeTransfers(SampleAccountData.Account3, SampleAccountData.Account1);
            await MakeTransfers(SampleAccountData.Account3, SampleAccountData.Account2);
        }

        private async Task MakeTransfers(AccountData inAccount, AccountData outAccount)
        {
            var actor = _accountActorFactory.Create(inAccount.Id);

            for (var i = 0; i < 10; i++)
            {
                var amount = new Money((decimal)random.NextDouble() * 10);

                try
                {
                    await actor.MakeTransaction(outAccount.Id, DateTimeOffset.Now, amount);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}