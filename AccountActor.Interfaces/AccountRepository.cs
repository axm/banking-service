using Accounts.Domain;
using Banking.Domain;
using System;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Base.Types;

namespace AccountActor.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountData> Get(AccountGuid id);
        Task<bool> Withdraw(AccountGuid id, Money amount);
        Task Deposit(AccountGuid id, Money amount);
        Task Transfer(AccountGuid id, AccountGuid to, Money amount);
        Task SetOverdraft(AccountGuid id, Money amount);
        Task<IEnumerable<NewTransaction>> GetTransactions(AccountGuid id, DateTimeOffset? fromDate = null);
        Task PutBalance(AccountGuid accountGuid, Money balance);
        Task PostDirectDebit(DirectDebit directDebit);
        Task DeleteDirectDebit(DirectDebitGuid directDebitId);
        Task StoreTransaction(NewTransaction transaction);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;
        private readonly string _mongoConnectionString;
        private readonly MongoClient _mongoClient;

        public AccountRepository(string connectionString, string mongoConnectionString)
        {
            _connectionString = connectionString;
            _mongoConnectionString = mongoConnectionString;
            _mongoClient = new MongoClient(_mongoConnectionString);
        }

        public async Task Deposit(AccountGuid id, Money amount)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                await sql.ExecuteAsync(Sql.Deposit, new { AccountId = id.Id, Amount = amount.Amount });
            }

            var deposits = _mongoClient.GetDatabase("local").GetCollection<BsonDocument>("deposits");

            var document = new BsonDocument
            {

            };

            await deposits.InsertOneAsync(document);
        }

        public async Task<AccountData> Get(AccountGuid id)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                var reader = await sql.ExecuteReaderAsync(Sql.Get, new { AccountId = id.Id});

                while(reader.Read())
                {
                    var accountId = new AccountGuid(reader.GetGuid(0));
                    var sortCode = new SortCode(reader.GetString(1));
                    var overdraft = new Money(reader.GetDecimal(2));
                    var amount = new Money(reader.GetDecimal(3));

                    return new AccountData(accountId, sortCode, overdraft, amount, new List<NewTransaction>());
                }

                return null;
            }
        }

        public async Task PutBalance(AccountGuid accountGuid, Money balance)
        {
            throw new NotImplementedException();
        }

        public async Task PostDirectDebit(DirectDebit directDebit)
        {
            using(var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync("Account.spSetDirectDebit", new {
                    Id = directDebit.Id.Id,
                    Amount = directDebit.Amount.Amount,
                    FromAccountId = directDebit.FromAccountId.Id,
                    ToAccountId = directDebit.ToAccountId.Id,
                    StartDate = directDebit.StartDate.UtcDateTime,
                    Frequency = (int)directDebit.Frequency }, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task DeleteDirectDebit(DirectDebitGuid directDebitId)
        {
            throw new NotImplementedException();
        }

        public async Task SetOverdraft(AccountGuid id, Money amount)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                await sql.ExecuteAsync(Sql.Transfer, new { Id = id.Id, Amount = amount }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task Transfer(AccountGuid from, AccountGuid to, Money amount)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                await sql.ExecuteAsync(Sql.Transfer, new { From = from.Id, To = to.Id, Amount = amount.Amount });
            }
        }

        public async Task<bool> Withdraw(AccountGuid id, Money amount)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                return await sql.ExecuteScalarAsync<bool>(Sql.Withdraw, new { AccountId = id.Id, Amount = amount.Amount });
            }
        }

        public async Task StoreTransaction(NewTransaction transaction)
        {
            var transactions = _mongoClient.GetDatabase("local").GetCollection<NewTransaction>("transactions");

            await transactions.InsertOneAsync(transaction);
        }

        public async Task<IEnumerable<NewTransaction>> GetTransactions(AccountGuid id, DateTimeOffset? fromDate = null)
        {
            var transactions = _mongoClient.GetDatabase("local").GetCollection<NewTransaction>("transactions");

            var cursor = await transactions.FindAsync(t => t.InputAccountId == id || t.OutputAccountId == id);

            return await cursor.ToListAsync();
        }
    }
}
