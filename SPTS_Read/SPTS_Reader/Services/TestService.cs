using SPTS_Reader.Data;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Services;
public class TestService : ITestService
{
    private ITestRepository _testRepo;

    public TestService(MongoDbContext context)
    {
        _testRepo = new TestRepository(context);
    }

    public async Task<List<Test>> GetBatchAsync(int limit, int skip)
    {
        return await _testRepo.GetBatchAsync(limit, skip);
    }

    public async Task<List<Test>> GetByAuthorIdAsync(string authorId)
    {
        return await _testRepo.GetByAuthorIdAsync(authorId);
    }

    public async Task<List<Test>> GetByTestMethodAsync(TestMethod method, int limit, int skip)
    {
        return await _testRepo.GetByTestMethodAsync(method, limit, skip);
    }

    public async Task<Test?> GetByIdAsync(Guid id)
    {
        return await _testRepo.GetByIdAsync(id);
    }

    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }
}
