using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace SPTS_Reader.Data;

public class TestRepository : Repository<Test>, ITestRepository
{
    public TestRepository(MongoDbContext context) : base(context) { }

    public async Task<List<Test>> GetBatchAsync(int limit, int skip)
    {
        return await _collection.Find(test => true).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<List<Test>> GetByAuthorIdAsync(string authorId)
    {
        return await _collection.Find(test => test.Author == authorId).ToListAsync();
    }

    public async Task<List<Test>> GetByTestMethodAsync(TestMethod method, int limit, int skip)
    {
        return await _collection.Find(test => test.Method == method).Skip(skip).Limit(limit).ToListAsync();
    }

    public async Task<Test?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(test => test.Id == id).FirstOrDefaultAsync();
    }

    public async Task<long> CountAsync()
    {
        return await _collection.CountDocumentsAsync(_ => true);
    }
}
