using SPTS_Reader.Data;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Services;
public class HistoryService : IHistoryService
{
    private IHistoryRepository _historyRepo;


    public HistoryService(MongoDbContext context)
    {
        _historyRepo = new HistoryRepository(context);
    }

    public Task AddHistoryAsync(History history)
    {
       return _historyRepo.AddAsync(history);
    }


    public async Task<long> CountAsync()
    {
        return await _historyRepo.CountAsync();
    }

    public async Task DeleteHistoryAsync(Guid id)
    {
        
        await _historyRepo.DeleteAsync(id.ToString());
    }

    public async Task<List<History>> GetBatchAsync(int limit, int skip)
    {
        return await _historyRepo.GetBatchAsync(limit, skip);
    }

    public async Task<History?> GetByIdAsync(Guid id)
    {
        return await _historyRepo.GetByIdAsync(id);
    }

    public async Task<List<History>> GetByUserIdAsync(Guid userId, int limit, int skip)
    {
        return await _historyRepo.GetByUserIdAsync(userId, limit, skip);
    }

    public async Task UpdateHistoryAsync(History history)
    {
        await _historyRepo.UpdateAsync(history.Id.ToString(),history) ;
    }
}

