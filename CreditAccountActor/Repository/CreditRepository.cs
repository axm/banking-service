using Credits.Interfaces;
using System;
using System.Threading.Tasks;
using Credits.Domain;
using System.Data.SqlClient;
using Dapper;
using CreditAccountActor.Repository;
using MongoDB.Driver;
using MongoDB.Bson;
using Base.Types;

namespace CreditAccountActor.Repository
{
    public class CreditRepository : ICreditRepository
    {
        private readonly string _connectionString;
        private readonly MongoClient _mongoClient;

        public CreditRepository(string connectionString)
        {
            _connectionString = connectionString;
            _mongoClient = new MongoClient("mongodb://localhost:27017");
        }

        public async Task BackupPayment(Payment payment)
        {
            var payments = _mongoClient.GetDatabase("local").GetCollection<BsonDocument>("payments");

            var document = new BsonDocument
            {

            };

            await payments.InsertOneAsync(document);
        }

        public async Task MakePayment(Payment payment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(Sql.MakePayment, new
                {
                    PaymentId = payment.PaymentId.Id,
                    CreditId = payment.CreditAccountId.Id,
                    FromAccountId = payment.FromAccountId.Id,
                    Timestamp = payment.Timestamp,
                    Amount = payment.Amount.Amount
                });
            }
        }

        public async Task MakeTransaction(Transaction transaction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(Sql.MakeTransaction, new
                {
                    TransactionId = transaction.TransactionId.Id,
                    CreditId = transaction.CreditAccountId.Id,
                    Timestamp = transaction.Timestamp,
                    Amount = transaction.Amount.Amount
                });
            }
        }

        public async Task BackupTransaction(Transaction transaction)
        {
            var transactions = _mongoClient.GetDatabase("local").GetCollection<BsonDocument>("transactions");

            var document = new BsonDocument
            {

            };

            await transactions.InsertOneAsync(document);
        }

        public async Task<CreditAccount> Get(CreditAccountGuid creditAccountId)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> GetTransactions()
        {
            throw new NotImplementedException();
        }

        public async Task CreateCreditStore()
        {
            await _mongoClient.GetDatabase("local").CreateCollectionAsync("payments");
            await _mongoClient.GetDatabase("local").GetCollection<Payment>("payments").Indexes.CreateOneAsync(Builders<Payment>.IndexKeys.Descending(_ => _.Timestamp));

            await _mongoClient.GetDatabase("local").CreateCollectionAsync("creditAccounts");
        }
    }
}
