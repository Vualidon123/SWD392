using SPTS_Writer.Entities;

namespace SPTS_Writer.Services.Abstraction
{
    public interface ISchoolService
    {
        public Task<School?> GetSchoolByIdAsync(string id);
        public Task<IEnumerable<School>> GetAllSchoolsAsync();
        public Task AddSchoolAsync(School school);
        public Task UpdateSchoolAsync(School school);
        public Task DeleteSchoolAsync(string id);

    }
}

