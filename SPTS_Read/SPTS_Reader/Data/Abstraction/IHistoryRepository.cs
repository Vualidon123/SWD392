using SPTS_Reader.Entities;

namespace SPTS_Reader.Data.Abstraction;
public interface IHistoryRepository : IRepository<History>
{
    Task<History?> GetByIdAsync(Guid id);
    Task<List<History>> GetByUserIdAsync(Guid userId, int limit, int skip);
    Task<List<History>> GetBatchAsync(int limit, int skip);
}

