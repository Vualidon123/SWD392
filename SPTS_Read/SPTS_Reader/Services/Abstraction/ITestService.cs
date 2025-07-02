using SPTS_Reader.Entities;

namespace SPTS_Reader.Services.Abstraction;
public interface ITestService
{
    Task<List<Test>> GetByAuthorIdAsync(string authorId);
    Task<List<Test>> GetByTestMethodAsync(TestMethod method, int limit, int skip);
    Task<List<Test>> GetBatchAsync(int limit, int skip);
    Task<Test?> GetByIdAsync(Guid id);
}

