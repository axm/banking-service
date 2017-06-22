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
using MongoDB.Bson;

namespace DirectDebitService
{
    public interface IDirectDebitRepository
    {
        Task<IEnumerable<DirectDebit>> GetDirectDebitsForToday();
        Task MarkDirectDebit(DirectDebitGuid id, DateTimeOffset timestamp);
    }

    public class DirectDebitRepository : IDirectDebitRepository
    {
        private readonly string _connectionString;
        private readonly IDateTimeService _dateTimeService;
        private readonly MongoClient _mongoClient;

        private IMongoCollection<DirectDebit> DebitCollection => _mongoClient.GetDatabase("local").GetCollection<DirectDebit>("directDebits");
        private IMongoCollection<BsonDocument> DebitCollection2 => _mongoClient.GetDatabase("local").GetCollection<BsonDocument>("directDebits");


        public DirectDebitRepository(string connectionString, IDateTimeService dateTimeService, MongoClient mongoClient)
        {
            _connectionString = connectionString;
            _dateTimeService = dateTimeService;
            _mongoClient = mongoClient;
        }

        public async Task<IEnumerable<DirectDebit>> GetDirectDebitsForToday()
        {
            var today = _dateTimeService.GetDateTimeOffset().Date;

            var directDebits = (await DebitCollection.FindAsync(d => true)).ToList();

            var directDebits1 = (await DebitCollection.FindAsync(d => d.StartDate.Date == today)).ToList();

            return directDebits;
        }

        public async Task MarkDirectDebit(DirectDebitGuid id, DateTimeOffset timestamp)
        {
            var debit = await DebitCollection.FindOneAndUpdateAsync((DirectDebit dd) => dd.Id == id, Builders<DirectDebit>.Update.Set("LastRunTimestamp", timestamp));
        }
    }
}
