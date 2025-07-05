using SPTS_Reader.Entities;

namespace SPTS_Reader.Data.Abstraction
{
    public interface ISchoolRepository : IRepository<School>
    {
        Task<List<School>> FindBySpecializationNameAsync(string specializationName);
    }
}