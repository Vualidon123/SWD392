using SPTS_Writer.Entities;

namespace SPTS_Writer.Services.Abstraction
{
    public interface ISchoolService
    {
        public Task<School?> GetHistoryByIdAsync(string id);
        public Task<IEnumerable<School>> GetAllHistorysAsync();
        public Task AddHistoryAsync(School school);
        public Task UpdateHistoryAsync(School school);
        public Task DeleteHistoryAsync(string id);

    }
}

