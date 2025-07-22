using SPTS_Writer.Entities;

namespace SPTS_Writer.Services.Abstraction
{
    public interface ISchoolService
    {
        public Task<School?> GetSchoolByIdAsync(string id);
        public Task<IEnumerable<School>> GetAllSchoolsAsync();
        public Task<string> AddSchoolAsync(School school);
        public Task UpdateSchoolAsync(School school);
        public Task DeleteSchoolAsync(string id);
        public Task<School?> GetSchoolByNameAsync(string name);
        public Task<School?> GetByIdAsync(string id);

    }
}

