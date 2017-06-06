using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingPeriodService.Repository
{
    public class BillingPeriodRepository : IBillingPeriodRepository
    {
        private readonly string _connectionString;

        public BillingPeriodRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateBillingPeriod(DateTime startDate, DateTime endDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("Credit.spCreateBillingPeriod", new { StartDate = startDate, EndDate = endDate });
            }
        }

        public async Task<BillingPeriod> GetCurrentBillingPeriod()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleAsync<BillingPeriod>("Credit.spGetCurrentBillingPeriod", commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
