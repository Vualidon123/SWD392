using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;

namespace SPTS_Writer.Services
{
    public class SchoolService
    {
        private readonly IRepository<School> _schoolRepository;
        public SchoolService(IRepository<School> schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }
        public async Task<School?> GetTestByIdAsync(string id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<School>> GetAllTestsAsync()
        {
            return await _schoolRepository.GetAllAsync();
        }

        public async Task AddTestAsync(School school)
        {
            school.Id = new Guid();
            await _schoolRepository.AddAsync(school);
            await _schoolRepository.SaveChangesAsync();
        }

        public async Task UpdateTestAsync(School school)
        {
            await _schoolRepository.UpdateAsync(school.Id.ToString(), school);
            await _schoolRepository.SaveChangesAsync();
        }
        public async Task DeleteTestAsync(string id)
        {
            await _schoolRepository.DeleteAsync(id);
            await _schoolRepository.SaveChangesAsync();
        }


    }
}
