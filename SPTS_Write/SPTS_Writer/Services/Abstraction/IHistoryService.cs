using SPTS_Writer.Entities;

namespace SPTS_Writer.Services.Abstraction;
public interface IHistoryService
{
    public Task<History?> GetHistoryByIdAsync(string id);
    public Task<IEnumerable<History>> GetAllHistorysAsync();
    public Task AddHistoryAsync(History test);
    public Task UpdateHistoryAsync(History test);
    public Task<History> RecordTakenTestAsync(Test test, User who, List<Answer> answers, TestStatus status);
}
