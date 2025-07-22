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
        public async Task<School?> GetSchoolByIdAsync(string id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await _schoolRepository.GetAllAsync();
        }

        public async Task AddSchoolAsync(School school)
        {
            school.Id = new Guid();
            await _schoolRepository.AddAsync(school);
            await _schoolRepository.SaveChangesAsync();
        }

        public async Task UpdateSchoolAsync(School school)
        {
            await _schoolRepository.UpdateAsync(school.Id.ToString(), school);
            await _schoolRepository.SaveChangesAsync();
        }
        public async Task DeleteSchoolAsync(string id)
        {
            await _schoolRepository.DeleteAsync(id);
            await _schoolRepository.SaveChangesAsync();
        }

    }
}
