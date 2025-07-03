using MongoDB.Driver;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;

namespace SPTS_Reader.Data
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(MongoDbContext context) : base(context) { }

        public async Task<IEnumerable<School>> FindBySpecializationNamesAsync(IEnumerable<string> specializationNames)
        {
            var filter = Builders<School>.Filter.ElemMatch(
                s => s.Specializations,
                Builders<Specializations>.Filter.In(spec => spec.Name, specializationNames)
            );
            return await FindAllAsync(filter);
        }
    }
}