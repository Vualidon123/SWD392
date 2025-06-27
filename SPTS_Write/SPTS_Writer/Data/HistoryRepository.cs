using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace SPTS_Writer.Data;

public class HistoryRepository : Repository<History>, IHistoryRepository
{
    public HistoryRepository(MongoDbContext context) : base(context) { }

    public async Task<History> CreateHistoryAsync(History takenTest)
    {
        await base.AddAsync(takenTest);
        await base.SaveChangesAsync();
        return takenTest;
    }

    public async Task<History> UpdateHistoryAsync(Guid oldId, History takenTest)
    {
        await base.UpdateAsync(oldId.ToString(), takenTest);
        await base.SaveChangesAsync();
        return takenTest;
    }

    public async Task<History?> GetByTestIdAndUserIdAsync(Guid testId, Guid userId)
    {
        var filter = Builders<History>.Filter.Eq(history => history.UserId, userId)
            & Builders<History>.Filter.Eq(history => history.TestId, testId);
        var result = await _collection.FindAsync(filter);
        return await result.FirstOrDefaultAsync();

    }
}
