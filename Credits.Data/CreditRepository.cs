using Credits.Interfaces;
using System;
using System.Threading.Tasks;
using Credits.Domain;
using System.Data.SqlClient;
using Dapper;

namespace Credits.Data
{
    public class CreditRepository : ICreditRepository
    {
        private readonly string _connectionString;

        public CreditRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task MakePayment(Payment payment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(Sql.MakePayment, new
                {
                    PaymentId = payment.PaymentId,
                    CreditId = payment.CreditAccountId,
                    Timestamp = payment.Timestamp,
                    Amount = payment.Amount
                });
            }
        }

        public async Task MakeTransaction(Transaction transaction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(Sql.MakeTransaction, new
                {
                    TransactionId = transaction.TransactionId,
                    CreditId = transaction.CreditAccountId,
                    Timestamp = transaction.Timestamp,
                    Amount = transaction.Amount
                });
            }
        }
    }
}
