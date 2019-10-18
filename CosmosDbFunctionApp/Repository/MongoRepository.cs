using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryContract;

using MongoDB;
using MongoDB.Driver;

namespace Repository
{
    public class MongoRepository<T> : IMongoRepository<T>
    {
        static protected MongoClient _client = null;

        protected IMongoConfig _config;
        protected IMongoDatabase _database = null;
        protected IMongoCollection<T> _collection = null;

        public MongoRepository(IMongoConfig config)
        {
            _config = config;

            _client = new MongoClient(_config.ConnectionStr);

            _database = _client.GetDatabase(_config.Database);

            _collection = _database.GetCollection<T>(_config.Collection);
        }

        public async Task Delete(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> func)
        {
            var result = await _collection.FindAsync<T>(func);

            return result.ToList<T>();
        }

        public async Task<T> Insert(T item)
        {
            await _collection.InsertOneAsync(item);

            return item;
        }

        public Task<T> Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
