using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credits.Domain
{
    public interface ITransactionFactory
    {
        Transaction NewTransaction(CreditAccountGuid creditAccountId, DateTimeOffset timestamp, decimal amount);
        Transaction NewTransaction(TransactionGuid id, CreditAccountGuid creditAccountId, DateTimeOffset timestamp, decimal amount);
    }

    public class TransactionFactory : ITransactionFactory
    {


        public Transaction NewTransaction(TransactionGuid id, CreditAccountGuid creditAccountId, DateTimeOffset timestamp, decimal amount)
        {
            throw new Exception();
        }

        public Transaction NewTransaction(CreditAccountGuid creditAccountId, DateTimeOffset timestamp, decimal amount)
        {
            return new Transaction
            {
                TransactionId = new TransactionGuid(),
                CreditAccountId = creditAccountId
            };
        }
    }
}
