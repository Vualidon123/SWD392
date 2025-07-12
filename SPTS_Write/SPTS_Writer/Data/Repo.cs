// ...existing code...
using MongoDB.Driver;
using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus;
using SPTS_Writer.Models;
using System.Linq.Expressions; // or SPTS_Writer.Entities if Test is there

public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        

        public Repository(MongoDbContext context)
        {
            _collection = context.GetCollection<T>();
           
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
           
        }

        public async Task UpdateAsync(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.ReplaceOneAsync(filter, entity);
            
        }

        public async Task DeleteAsync(string id)
        {
            T? entity = await GetByIdAsync(id);
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.DeleteOneAsync(filter);
           
        }

        public Task SaveChangesAsync()
        {
            // MongoDB does not require explicit save changes
            return Task.CompletedTask;
        }

	public async Task<long> CountAsync()
	{
		return await _collection.CountDocumentsAsync(Builders<T>.Filter.Empty);
	}
}
// ...existing code...