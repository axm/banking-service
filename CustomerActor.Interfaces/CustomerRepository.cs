using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerActor.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateCustomerStore();
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoClient _mongoClient;

        public CustomerRepository(string connectionString, string mongoConnectionString)
        {
            _mongoClient = new MongoClient(mongoConnectionString);
        }

        public async Task CreateCustomerStore()
        {
            await _mongoClient.GetDatabase("local").CreateCollectionAsync("customers");
        }
    }
}
