using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Services;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public IActionResult GetHistory()
        {
            var test = _schoolService.GetAllHistorysAsync().Result;
            return Ok(test);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryById(string id)
        {
            var test = await _schoolService.GetHistoryByIdAsync(id);
            if (test == null)
            {
                return NotFound(new { error = "There is no test with id: " + id });
            }
            return Ok(test);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> AddHistory(School test)
        {
            if (test == null)
            {
                return BadRequest(new { error = "History cannot be null" });
            }
            await _schoolService.AddHistoryAsync(test);
            return CreatedAtAction(nameof(GetHistoryById), new { id = test.Id }, test);
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> UpdateHistory(School test)
        {
            if (test == null)
            {
                return BadRequest(new { error = "History cannot be null" });
            }
            await _schoolService.UpdateHistoryAsync(test);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> DeleteHistory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { error = "Id cannot be null or empty" });
            }
            await _schoolService.DeleteHistoryAsync(id);
            return NoContent();
        }
    }
}
