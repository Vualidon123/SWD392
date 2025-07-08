using SPTS_Reader.Entities;

namespace SPTS_Reader.Services.Abstraction;
public interface IHistoryService
{
    Task<History?> GetByIdAsync(Guid id);
    Task<List<History>> GetByUserIdAsync(Guid userId, int limit, int skip);
    Task<List<History>> GetBatchAsync(int limit, int skip);
    Task<long> CountAsync();
    Task AddHistoryAsync(History history);
    Task UpdateHistoryAsync(History history);
    Task DeleteHistoryAsync(Guid id);
}


