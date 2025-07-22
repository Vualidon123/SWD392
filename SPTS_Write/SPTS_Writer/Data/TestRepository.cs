using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace SPTS_Writer.Data;

public class TestRepository : Repository<Test>, ITestRepository
{
    public TestRepository(MongoDbContext context) : base(context) { }

    public async Task<Test> CreateTestAsync(Test takenTest)
    {
        await base.AddAsync(takenTest);
        await base.SaveChangesAsync();
        return takenTest;
    }

    public async Task<IEnumerable<Test>> GetRandomizedTests(Guid userId)
    {
        return await _collection.Find(Builders<Test>.Filter.Eq("IsRandomized", userId)).ToListAsync();
    }

    public async Task<Test> UpdateTestAsync(Guid oldId, Test takenTest)
    {
        await base.UpdateAsync(oldId.ToString(), takenTest);
        await base.SaveChangesAsync();
        return takenTest;
    }

}

