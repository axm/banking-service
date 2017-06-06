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

namespace BankingIntegrationTests.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    [IntegrationTestController]
    public class AccountsController : Controller
    {
        [BeforeAll]
        [IntegrationTest]
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
    }
}