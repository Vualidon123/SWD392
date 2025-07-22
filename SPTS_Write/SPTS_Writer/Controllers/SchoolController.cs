using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Services;

namespace SPTS_Writer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolService _schoolService;

        public SchoolController(SchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public IActionResult GetTest()
        {
            var test = _schoolService.GetAllTestsAsync().Result;
            return Ok(test);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(string id)
        {
            var test = await _schoolService.GetTestByIdAsync(id);
            if (test == null)
            {
                return NotFound(new { error = "There is no test with id: " + id });
            }
            return Ok(test);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> AddTest(School test)
        {
            if (test == null)
            {
                return BadRequest(new { error = "Test cannot be null" });
            }
            await _schoolService.AddTestAsync(test);
            return CreatedAtAction(nameof(GetTestById), new { id = test.Id }, test);
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> UpdateTest(School test)
        {
            if (test == null)
            {
                return BadRequest(new { error = "Test cannot be null" });
            }
            await _schoolService.UpdateTestAsync(test);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> DeleteTest(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { error = "Id cannot be null or empty" });
            }
            await _schoolService.DeleteTestAsync(id);
            return NoContent();
        }
    }
}
