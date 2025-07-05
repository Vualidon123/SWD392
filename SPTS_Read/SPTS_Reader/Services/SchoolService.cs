using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository =  schoolRepository;
        }
        public async Task<School?> GetSchoolByIdAsync(string id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await _schoolRepository.GetAllAsync();
        }

        public async Task AddSchoolAsync(School test)
        {
            await _schoolRepository.AddAsync(test);
            await _schoolRepository.SaveChangesAsync();
        }

        public async Task UpdateSchoolAsync(School test)
        {
            await _schoolRepository.UpdateAsync(test.Id.ToString(), test);
            await _schoolRepository.SaveChangesAsync();
        }
        public async Task DeleteSchoolAsync(string id)
        {
            await _schoolRepository.DeleteAsync(id);
            await _schoolRepository.SaveChangesAsync();
        }

    }
}
