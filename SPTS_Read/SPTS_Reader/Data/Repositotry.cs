using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;

namespace SPTS_Reader.Data;
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;

    public Repository(MongoDbContext context) => _collection = context.GetCollection<T>();
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

    public async Task<IEnumerable<T>> FindAllAsync(FilterDefinition<T> filter)
    {
        return await _collection.Find(filter).ToListAsync();
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
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }

    public Task SaveChangesAsync()
    {
        // MongoDB does not require explicit save changes
        return Task.CompletedTask;
    }

    public virtual async Task<long> CountAsync()
    {
        return await _collection.CountDocumentsAsync(_ => true);
    }
}
