using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using SPTS_Reader.Models;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Services
{
    public class SpecializationsRecommendationService : ISpecializationsRecommendationService
    {
        private readonly ISpecializationsRecommendationRepository _specialsRecommendRepositoryRepo;
        private readonly ISchoolRepository _schoolRepo;

        public SpecializationsRecommendationService(ISpecializationsRecommendationRepository specialsRecommendRepositoryRepo, ISchoolRepository schoolRepo)
        {
            _specialsRecommendRepositoryRepo = specialsRecommendRepositoryRepo;
            _schoolRepo = schoolRepo;
        }

        public async Task<List<RecommendModel>> GetRecommendationFromPersonality(string personality)
        {

            var recommendationList = await _specialsRecommendRepositoryRepo.GetRecommendationsByPersonalityAsync(personality);

            List<RecommendModel> recommendModelList = new List<RecommendModel>();
            foreach (var recommendation in recommendationList)
            {
                var schools = await _schoolRepo.FindBySpecializationNameAsync(recommendation.SpecializationName);

                foreach (var school in schools)
                {
                    school.Specializations = school.Specializations
                        .Where(s => s.Name == recommendation.SpecializationName)
                        .ToArray();
                }

                recommendModelList.Add(new RecommendModel
                {
                    Specialization = recommendation.SpecializationName,
                    Schools = schools
                });
            }

            return recommendModelList;
        }
    }

}