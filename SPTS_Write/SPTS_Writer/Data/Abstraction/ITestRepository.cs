using SPTS_Writer.Entities;

namespace SPTS_Writer.Data.Abstraction;
public interface ITestRepository : IRepository<Test>
{
    public Task<IEnumerable<Test>> GetRandomizedTests(Guid userId);
}
