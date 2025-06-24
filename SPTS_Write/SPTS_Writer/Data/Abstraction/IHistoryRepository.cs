using SPTS_Writer.Entities;

namespace SPTS_Writer.Data.Abstraction;
public interface IHistoryRepository : IRepository<History>
{
    Task<History> CreateHistoryAsync(History taken);
}

