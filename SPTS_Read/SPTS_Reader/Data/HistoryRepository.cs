using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace SPTS_Reader.Data;

public class HistoryRepository : Repository<History>, IHistoryRepository
{
    public HistoryRepository(MongoDbContext context) : base(context) { }

   

    public async Task<List<History>> GetBatchAsync(int limit, int skip)
    {
        return await _collection.Find(history => true).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<History?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(history => history.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<History>> GetByUserIdAsync(Guid userId, int limit, int skip)
    {
        return await _collection.Find(history => history.UserId == userId).Skip(skip).Limit(limit).ToListAsync();
    }
}

