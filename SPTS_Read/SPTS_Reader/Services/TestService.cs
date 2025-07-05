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

    public async Task<long> CountAsync()
    {
        return await _testRepo.CountAsync();
    }
    public async Task<Test?> GetTestByIdAsync(string id)
    {
        return await _testRepo.GetByIdAsync(id);
    }
    public async Task<IEnumerable<Test>> GetAllTestsAsync()
    {
        return await _testRepo.GetAllAsync();
    }

    public async Task AddTestAsync(Test test)
    {
        await _testRepo.AddAsync(test);
        await _testRepo.SaveChangesAsync();
    }

    public async Task UpdateTestAsync(Test test)
    {
        await _testRepo.UpdateAsync(test.Id.ToString(), test);
        await _testRepo.SaveChangesAsync();
    }
    public async Task DeleteTestAsync(string id)
    {
        await _testRepo.DeleteAsync(id);
        await _testRepo.SaveChangesAsync();
    }
}
