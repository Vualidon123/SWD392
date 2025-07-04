using SPTS_Reader.Entities;

namespace SPTS_Reader.Data.Abstraction
{
    public interface ISpecializationsRecommendationRepository : IRepository<SpecializationsRecommendation>
    {
        Task<IEnumerable<SpecializationsRecommendation>> GetRecommendationsByPersonalityAsync(string personality);
    }
}