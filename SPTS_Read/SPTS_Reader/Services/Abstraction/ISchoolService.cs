using SPTS_Reader.Entities;

namespace SPTS_Reader.Services.Abstraction
{
    public interface ISchoolService
    {
        Task<School?> GetSchoolByIdAsync(string id);

        Task<IEnumerable<School>> GetAllSchoolsAsync();
        Task AddSchoolAsync(School school);
        Task UpdateSchoolAsync(School school);
        Task DeleteSchoolAsync(string id);
    }
}
