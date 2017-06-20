using Dapper;
using Accounts.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Domain;
using Base.Types;
using MongoDB.Driver;

namespace DirectDebitService
{
    public interface IDirectDebitRepository
    {
        Task<IEnumerable<DirectDebit>> GetDirectDebitsForToday();
    }

    public class DirectDebitRepository : IDirectDebitRepository
    {
        private readonly string _connectionString;
        private readonly IDateTimeService _dateTimeService;
        private readonly MongoClient _mongoClient;

        public DirectDebitRepository(string connectionString, IDateTimeService dateTimeService, MongoClient mongoClient)
        {
            _connectionString = connectionString;
            _dateTimeService = dateTimeService;
            _mongoClient = mongoClient;
        }

        public async Task<IEnumerable<DirectDebit>> GetDirectDebitsForToday()
        {
            var today = _dateTimeService.GetDateTimeOffset().Date;

            var debitCollection = _mongoClient.GetDatabase("local").GetCollection<DirectDebit>("directDebit");

            var directDebits = await debitCollection.FindAsync(d => d.StartDate.Date == today);

            return directDebits.ToEnumerable();
        }
    }
}
