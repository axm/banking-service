using Credits.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credits.Interfaces.Validations
{
    public interface IPaymentValidation
    {
        PaymentValidationSpecifier Specifier { get; set; }
    }
}
