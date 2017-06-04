using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankingIntegrationTests.Attributes;

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
            Console.WriteLine($"Running setup: ${nameof(Setup)}");
        }
    }
}