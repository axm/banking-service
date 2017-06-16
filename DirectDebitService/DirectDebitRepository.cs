using Dapper;
using Accounts.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitService
{
    public interface IDirectDebitRepository
    {
        Task<IEnumerable<DirectDebit>> GetDirectDebits();
    }

    public class DirectDebitRepository : IDirectDebitRepository
    {
        private readonly string _connectionString;

        public DirectDebitRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DirectDebit>> GetDirectDebits()
        {
            using(var sqlConnection = new SqlConnection(_connectionString))
            {
                return await sqlConnection.QueryAsync<DirectDebit>("Account.spGetDirectDebits", commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
