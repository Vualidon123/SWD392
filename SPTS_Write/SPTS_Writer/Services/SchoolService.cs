using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }
        public async Task<School?> GetHistoryByIdAsync(string id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<School>> GetAllHistorysAsync()
        {
            return await _schoolRepository.GetAllAsync();
        }

        public async Task AddHistoryAsync(School school)
        {
            school.Id = new Guid();
            await _schoolRepository.AddAsync(school);
            await _schoolRepository.SaveChangesAsync();
        }

        public async Task UpdateHistoryAsync(School school)
        {
            await _schoolRepository.UpdateAsync(school.Id.ToString(), school);
            await _schoolRepository.SaveChangesAsync();
        }
        public async Task DeleteHistoryAsync(string id)
        {
            await _schoolRepository.DeleteAsync(id);
            await _schoolRepository.SaveChangesAsync();
        }

    }
}
