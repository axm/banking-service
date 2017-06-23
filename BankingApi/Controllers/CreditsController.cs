using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreditAccountActor.Interfaces;
using BankingApi.Validation;
using Base.Types;

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

            var id = new CreditAccountGuid(makePaymentParams.CreditAccountId);
            var actor = _creditAccountActorFactory.Create(id);

            await actor.MakePayment(new CreditAccountActor.Interfaces.Params.PaymentParams
            {
                Id = id,
                FromAccountId = new AccountGuid(makePaymentParams.AccountId),
                Amount = new Money(makePaymentParams.Amount),
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

            var id = new CreditAccountGuid(makeTransactionParams.CreditAccountId);
            var actor = _creditAccountActorFactory.Create(id);

            await actor.MakeTransaction(new CreditAccountActor.Interfaces.Params.TransactionParams
            {
                Id = id,
                Amount = new Money(makeTransactionParams.Amount),
                Timestamp = DateTime.Now 
            });
        }

        [HttpPost("SetInterest")]
        public async Task SetInterest(object param)
        {
            throw new NotImplementedException();
        }
    }
}