using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CreditAccountActor.Interfaces;
using CreditTransactionsActor.Interfaces;
using CreditPaymentsActor.Interfaces;
using Credits.Domain;
using CreditAccountActor.Interfaces.Params;
using BankingIntegrationTests.Attributes;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace BankingIntegrationTests.Controllers
{
    [Produces("application/json")]
    [Route("api/Credits")]
    [IntegrationTestControllerAttribute]
    public class CreditsController : Controller
    {
        private readonly ICreditAccountActorFactory _creditAccountActorFactory;
        private readonly ICreditTransactionsActorFactory _creditTransactionsActorFactory;
        private readonly ICreditPaymentsActorFactory _creditPaymentsActorFactory;

        public CreditsController(ICreditAccountActorFactory creditAccountActorFactory,
            ICreditTransactionsActorFactory creditTransactionsActorFactory,
            ICreditPaymentsActorFactory creditPaymentsActorFactory)
        {
            _creditAccountActorFactory = creditAccountActorFactory;
            _creditTransactionsActorFactory = creditTransactionsActorFactory;
            _creditPaymentsActorFactory = creditPaymentsActorFactory;
        }

        [BeforeAll]
        public async Task Setup()
        {
        }

        [HttpGet]
        //[IntegrationTest]
        public async Task MakePayment()
        {
            var creditAccountId = new CreditAccountGuid(new Guid("1FF8A281-026B-4C58-AA67-8E3657DD21B4"));
            var payment = new PaymentParams
            {
                Id = creditAccountId
            };
            
            var actor = _creditAccountActorFactory.Create(creditAccountId);

            await actor.MakePayment(payment);
        }

        [HttpGet]
        [IntegrationTest]
        public async Task MakePayments()
        {
            Console.WriteLine($"Running {nameof(MakePayments)}");
        }

        [HttpGet]
        //[IntegrationTest]
        public async Task MakeTransaction()
        {
            var creditAccountId = new CreditAccountGuid(new Guid("1FF8A281-026B-4C58-AA67-8E3657DD21B4"));
            var transaction = new TransactionParams
            {
                Id = creditAccountId
            };

            var actor = _creditAccountActorFactory.Create(creditAccountId);

            await actor.MakeTransaction(transaction);
        }

        [HttpGet]
        [IntegrationTest]
        public async Task MakeTransactions()
        {
            Console.WriteLine($"Running {nameof(MakeTransactions)}");
        }
    }
}