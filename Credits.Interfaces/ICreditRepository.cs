using Credits.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credits.Interfaces
{
    public interface ICreditRepository
    {
        Task MakePayment(Payment payment);
        Task MakeTransaction(Transaction transactionParams);
    }
}
