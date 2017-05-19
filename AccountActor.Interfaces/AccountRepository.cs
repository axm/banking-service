using Accounts.Domain;
using Banking.Domain;
using System;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace AccountActor.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountData> Get(AccountGuid id);
        Task<bool> Withdraw(AccountGuid id, Money amount);
        Task Deposit(AccountGuid id, Money amount);
        Task Transfer(AccountGuid id, AccountGuid to, Money amount);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Deposit(AccountGuid id, Money amount)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                await sql.ExecuteAsync(Sql.Deposit, new { AccountId = id.Id, Amount = amount.Amount });
            }
        }

        public async Task<AccountData> Get(AccountGuid id)
        {
            using (var sql = new SqlConnection(_connectionString))
            {
                var reader = await sql.ExecuteReaderAsync(Sql.Get, new { AccountId = id.Id});

                while(reader.Read())
                {
                    var accountId = new AccountGuid(reader.GetGuid(0));
                    var amount = new Money(reader.GetDecimal(1));

                    return new AccountData(accountId, amount);
                }

                return null;
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
    }
}
