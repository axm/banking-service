using Banking.Domain;
using BankingApi.Controllers;
using CreditAccountActor.Interfaces.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Validation
{
    public class TransactionValidator : ITransactionValidator
    {
        public readonly ValidatorSpecifier Specifier = new ValidatorSpecifier(new ValidatorGuid(new Guid("d9e9c17e-52c9-40e9-a8d4-f7258f2600f0")), 1);

        public bool Validate(MakeTransactionParams transactionParams)
        {
            if(transactionParams == null)
            {
                throw new ArgumentNullException($"{nameof(transactionParams)} cannot be null.");
            }

            if(transactionParams.Amount <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(transactionParams.Amount)} cannot be zero or negative. Actual value: {transactionParams.Amount}.");
            }

            return true;
        }
    }
}
