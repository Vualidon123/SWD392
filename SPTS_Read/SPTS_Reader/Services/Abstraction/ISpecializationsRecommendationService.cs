using SPTS_Reader.Models;

namespace SPTS_Reader.Services.Abstraction
{
    public interface ISpecializationsRecommendationService
    {
        Task<List<RecommendModel>> GetRecommendationFromPersonality(string personality);
    }
}