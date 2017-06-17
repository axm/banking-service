using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankingIntegrationTests.Attributes;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using BankingIntegrationTests.Data;
using Accounts.Domain;
using AccountActor.Interfaces;
using Banking.Domain;

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
            
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync("Account.spNewAccount", new {
                    AccountId = SampleAccountData.Account1.Id.Id,
                    SortCode = SampleAccountData.Account1.SortCode.Code,
                    Overdraft = SampleAccountData.Account1.Overdraft.Amount,
                    Balance = SampleAccountData.Account1.Balance.Amount }, commandType: System.Data.CommandType.StoredProcedure);
                await connection.ExecuteAsync("Account.spNewAccount", new {
                    AccountId = SampleAccountData.Account2.Id.Id,
                    SortCode = SampleAccountData.Account2.SortCode.Code,
                    Overdraft = SampleAccountData.Account2.Overdraft.Amount,
                    Balance = SampleAccountData.Account2.Balance.Amount }, commandType: System.Data.CommandType.StoredProcedure);
                await connection.ExecuteAsync("Account.spNewAccount", new {
                    AccountId = SampleAccountData.Account3.Id.Id,
                    SortCode = SampleAccountData.Account3.SortCode.Code,
                    Overdraft = SampleAccountData.Account3.Overdraft.Amount,
                    Balance = SampleAccountData.Account3.Balance.Amount }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        [IntegrationTest]
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
                    await actor.MakeTransaction(null, account.Id, DateTimeOffset.Now, amount);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        [IntegrationTest]
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

                    await actor.MakeTransaction(account.Id, null, DateTimeOffset.Now, amount);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        [IntegrationTest]
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
                    await actor.MakeTransaction(inAccount.Id, outAccount.Id, DateTimeOffset.Now, amount);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}