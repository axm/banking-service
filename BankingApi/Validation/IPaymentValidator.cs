using BankingApi.Controllers;

namespace BankingApi.Validation
{
    public interface IPaymentValidator
    {
        bool Validate(MakePaymentParams makePaymentParams);
    }
}