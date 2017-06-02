using BankingApi.Controllers;
using CreditAccountActor.Interfaces.Params;

namespace BankingApi.Validation
{
    public interface ITransactionValidator
    {
        bool Validate(MakeTransactionParams transactionParams);
    }
}