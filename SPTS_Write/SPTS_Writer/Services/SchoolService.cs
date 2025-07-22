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

        public async Task<School?> GetSchoolByNameAsync(string name)
        {
            return await _schoolRepository.GetSchoolByNameAsync(name);
        }

        public async Task<School?> GetSchoolByIdAsync(string id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await _schoolRepository.GetAllAsync();
        }

        public async Task<string> AddSchoolAsync(School school)
        {
            school.Id = new Guid();
            var temp = _schoolRepository.GetSchoolByNameAsync(school.Name);
            if (temp != null)
                return "Name existed";
            await _schoolRepository.AddAsync(school);
            return "Ok";
        }

        public async Task UpdateSchoolAsync(School school)
        {
            await _schoolRepository.UpdateAsync(school.Id.ToString(), school);
        }

        public async Task DeleteSchoolAsync(string id)
        {
            await _schoolRepository.DeleteAsync(id);
        }

        public async Task<School?> GetByIdAsync(string id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }

    }
}
