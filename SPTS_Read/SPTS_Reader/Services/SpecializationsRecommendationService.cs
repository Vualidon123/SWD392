using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using SPTS_Reader.Models;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Services
{
    public class SpecializationsRecommendationService : ISpecializationsRecommendationService
    {
        private readonly ISpecializationsRecommendationRepository _specialsRecommendRepositoryRepo;
        private readonly IRepository<School> _schoolRepo;


        public SpecializationsRecommendationService(ISpecializationsRecommendationRepository specialsRecommendRepositoryRepo, IRepository<School> schoolRepo)
        {
            _specialsRecommendRepositoryRepo = specialsRecommendRepositoryRepo;
            _schoolRepo = schoolRepo;
        }

        
    }

}