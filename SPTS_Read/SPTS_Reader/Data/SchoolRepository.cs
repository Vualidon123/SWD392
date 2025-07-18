using MongoDB.Driver;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;

namespace SPTS_Reader.Data
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(MongoDbContext context) : base(context) { }

        public async Task<List<School>> FindBySpecializationNameAsync(string specializationName)
        {
            var filter = Builders<School>.Filter.ElemMatch(
                s => s.Specializations,
                Builders<Specializations>.Filter.Eq(spec => spec.Name, specializationName)
            );

            var sort = Builders<School>.Sort.Ascending(s => s.Ranking == 0 ? int.MaxValue : s.Ranking);

            var schools = await FindAllAsync(filter);
            return schools.ToList();
        }
    }
}