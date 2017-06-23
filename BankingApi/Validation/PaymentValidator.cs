using BankingApi.Controllers;
using Base.Types;
using System;

namespace BankingApi.Validation
{
    public class PaymentValidator : IPaymentValidator
    {
        public readonly ValidatorSpecifier Specifier = new ValidatorSpecifier(new ValidatorGuid(new Guid("1a40f45f-4755-49c5-804b-5dbd65920b71")), 1);

        public bool Validate(MakePaymentParams makePaymentParams)
        {
            if(makePaymentParams == null)
            {
                throw new ArgumentNullException($"{nameof(makePaymentParams)} cannot be null.");
            }

            if(makePaymentParams.Amount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
