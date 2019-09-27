using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Microsoft.Azure.Cosmos;



using RepositoryContract;

namespace Repository
{
    public class SQLRepository<T> : IRepository<T>
    {
        protected CosmosClient _client;

        protected ISqlConfig _config;
        protected Database _database;
        protected Container _container;

        public SQLRepository(ISqlConfig config)
        {
            _config = config;

            _client = new CosmosClient(config.EndPointUri, config.PrimaryKey);

            _database = _client.CreateDatabaseIfNotExistsAsync(_config.DataBase).Result.Database;

            _container = _database.CreateContainerIfNotExistsAsync(_config.Container, _config.PartitionKey).Result.Container;
        }

        public async Task Delete(string uniqueId)
        {
            await _container.DeleteItemAsync<T>(uniqueId, new PartitionKey(  _config.PartitionKey) );
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> func)
        {
            
            return _container.GetItemLinqQueryable<T>(true).Where(func).ToList<T>();
        }

        public async Task<T> Insert(T item)
        {
            await _container.CreateItemAsync<T>(item);

            return item;
        }

        public async Task<T> Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
