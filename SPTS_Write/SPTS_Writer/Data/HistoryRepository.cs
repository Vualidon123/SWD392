using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;

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
}
