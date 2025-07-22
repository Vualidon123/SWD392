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
        public IActionResult GetSchool()
        {
            var test = _schoolService.GetAllSchoolsAsync().Result;
            return Ok(test);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolById(string id)
        {
            var test = await _schoolService.GetSchoolByIdAsync(id);
            if (test == null)
            {
                return NotFound(new { error = "There is no test with id: " + id });
            }
            return Ok(test);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> AddSchool(School test)
        {
            if (test == null)
            {
                return BadRequest(new { error = "School cannot be null" });
            }
            await _schoolService.AddSchoolAsync(test);
            return CreatedAtAction(nameof(GetSchoolById), new { id = test.Id }, test);
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> UpdateSchool(School test)
        {
            if (test == null)
            {
                return BadRequest(new { error = "School cannot be null" });
            }
            await _schoolService.UpdateSchoolAsync(test);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> DeleteSchool(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { error = "Id cannot be null or empty" });
            }
            await _schoolService.DeleteSchoolAsync(id);
            return NoContent();
        }
    }
}
