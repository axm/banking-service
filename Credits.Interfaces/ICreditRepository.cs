using Base.Types;
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
        Task BackupPayment(Payment payment);
        Task MakePayment(Payment payment);
        Task MakeTransaction(Transaction transaction);
        Task BackupTransaction(Transaction transaction);
        Task<CreditAccount> Get(CreditAccountGuid creditAccountId);
        Task<Transaction> GetTransactions();
        Task CreateCreditStore();
    }
}
