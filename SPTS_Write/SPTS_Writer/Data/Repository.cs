using MongoDB.Bson;
using MongoDB.Driver;
using SPTS_Writer.Data.Abstraction;
using System.Linq.Expressions;

namespace SPTS_Writer.Data
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public Repository(MongoDbContext context) => _collection = context.GetCollection<T>();

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public virtual async Task UpdateAsync(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.DeleteOneAsync(filter);
        }

        public virtual Task SaveChangesAsync()
        {
            // MongoDB does not require explicit save changes
            return Task.CompletedTask;
        }

        public virtual async Task<long> CountAsync()
        {
            return await _collection.CountDocumentsAsync(_ => true);
        }

    }

}


