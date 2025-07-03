using MongoDB.Driver;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;

namespace SPTS_Reader.Data
{
    public class SpecializationsRecommendationRepository : Repository<SpecializationsRecommendation>, ISpecializationsRecommendationRepository
    {
        public SpecializationsRecommendationRepository(MongoDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SpecializationsRecommendation>> GetRecommendationsByPersonalityAsync(string personality)
        {
            var filter = Builders<SpecializationsRecommendation>.Filter.AnyStringIn(s => s.RecommendPersonality, personality);
            return await _collection.Find(filter).ToListAsync();
        }

    }
}