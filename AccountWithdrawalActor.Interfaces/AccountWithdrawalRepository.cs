using Accounts.Domain;
using Banking.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountWithdrawalActor.Interfaces
{
    public interface IAccountWithdrawalRepository
    {
        Task Withdraw(AccountGuid accountId, Money amount);
        Task<IEnumerable<Withdrawal>> Get(AccountGuid accountId, MonthYear monthYear);
    }

    public class AccountWithdrawalRepository : IAccountWithdrawalRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<BsonDocument> _withdrawalsCollection;

        public AccountWithdrawalRepository(string mongoConnectionString)
        {
            _mongoClient = new MongoClient(mongoConnectionString);
            _withdrawalsCollection = _mongoClient.GetDatabase("local").GetCollection<BsonDocument>("withdrawals");
        }

        public async Task<IEnumerable<Withdrawal>> Get(AccountGuid accountId, MonthYear monthYear)
        {
            throw new NotImplementedException();
        }

        public async Task Withdraw(AccountGuid accountId, Money amount)
        {
            var document = new BsonDocument(new Dictionary<string, object>() { 
                ["AccountId"] = accountId,
                ["Amount"] = amount
            });

            await _withdrawalsCollection.InsertOneAsync(document);
        }
    }
}
