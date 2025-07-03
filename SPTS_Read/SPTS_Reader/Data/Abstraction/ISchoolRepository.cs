using SPTS_Reader.Entities;

namespace SPTS_Reader.Data.Abstraction
{
    public interface ISchoolRepository : IRepository<School>
    {
        Task<IEnumerable<School>> FindBySpecializationNamesAsync(IEnumerable<string> specializationNames);
    }
}