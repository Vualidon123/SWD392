using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationsRecommendationController : ControllerBase
    {
        private readonly ISpecializationsRecommendationService _recommendationService;

        public SpecializationsRecommendationController(ISpecializationsRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        /// <summary>
        /// Get recommended specializations and schools by personality type.
        /// </summary>
        /// <param name="personality">Personality type (e.g., MBTI, DISC, etc.)</param>
        /// <returns>List of recommendations</returns>
        [HttpGet("recommend")]
        public async Task<IActionResult> GetRecommendations([FromQuery] string personality)
        {
            if (string.IsNullOrWhiteSpace(personality))
                return BadRequest(new { error = "Personality is required." });

            var result = await _recommendationService.GetRecommendationFromPersonality(personality);
            return Ok(result);
        }
    }
}