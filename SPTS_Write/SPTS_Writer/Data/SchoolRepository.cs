using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace SPTS_Writer.Data;

public class SchoolRepository : Repository<School>, ISchoolRepository
{
    public SchoolRepository(MongoDbContext context) : base(context) { }

    public async Task<School?> GetSchoolByNameAsync(string name)
    {
        var filter = Builders<School>.Filter.Eq("Name", name);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
}

