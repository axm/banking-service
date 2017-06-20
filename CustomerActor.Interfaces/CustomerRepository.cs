using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Domain;

namespace CustomerActor.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateCustomerStore();
        Task PutCustomer(Customer.Domain.Customer customer);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public CustomerRepository(string connectionString, string mongoConnectionString)
        {
            _mongoClient = new MongoClient(mongoConnectionString);
            _database = _mongoClient.GetDatabase("local");
        }

        public async Task PutCustomer(Customer.Domain.Customer customer)
        {
            await _database.GetCollection<Customer.Domain.Customer>("customers").InsertOneAsync(customer);
        }

        public async Task CreateCustomerStore()
        {
            await _database.CreateCollectionAsync("customers");
        }
    }
}
