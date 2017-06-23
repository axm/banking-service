using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Providers
{
    public interface IElasticSearchProvider
    {

    }

    public class ElasticSearchProvider : IElasticSearchProvider
    {
        private readonly ElasticClient _client;

        public ElasticSearchProvider(string connectionString)
        {
            _client = new ElasticClient(new Uri(connectionString));
        }
    }
}
