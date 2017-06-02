using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CreditAccountActor.Interfaces;
using Accounts.Domain;
using BankingApi.Validation;

namespace BankingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Credits")]
    public class CreditsController : Controller
    {
        private readonly ICreditAccountActorFactory _creditAccountActorFactory;
        private readonly IPaymentValidator _paymentValidator;
        private readonly ITransactionValidator _transactionValidator;

        public CreditsController(ICreditAccountActorFactory creditAccountActorFactory, ITransactionValidator transactionValidator, IPaymentValidator paymentValidator)
        {
            _creditAccountActorFactory = creditAccountActorFactory;
            _transactionValidator = transactionValidator;
            _paymentValidator = paymentValidator;
        }

        [HttpPost("MakePayment")]
        public async Task MakePayment([FromBody]MakePaymentParams makePaymentParams)
        {
            if(!_paymentValidator.Validate(makePaymentParams))
            {
                throw new Exception("Invalid parameters.");
            }

            var id = new Credits.Domain.CreditAccountGuid(makePaymentParams.CreditAccountId);
            var actor = _creditAccountActorFactory.Create(id);

            await actor.MakePayment(new CreditAccountActor.Interfaces.Params.PaymentParams
            {
                Id = id,
                FromAccountId = new AccountGuid(makePaymentParams.AccountId),
                Amount = makePaymentParams.Amount,
                Timestamp = DateTime.Now
            });
        }

        [HttpPost("MakeTransaction")]
        public async Task MakeTransaction([FromBody]MakeTransactionParams makeTransactionParams)
        {
            if(!_transactionValidator.Validate(makeTransactionParams))
            {
                throw new Exception("Invalid parameters.");
            }

            var id = new Credits.Domain.CreditAccountGuid(makeTransactionParams.CreditAccountId);
            var actor = _creditAccountActorFactory.Create(id);

            await actor.MakeTransaction(new CreditAccountActor.Interfaces.Params.TransactionParams
            {
                Id = id,
                Amount = makeTransactionParams.Amount,
                Timestamp = DateTime.Now 
            });
        }
    }
}