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
            var schools = _schoolService.GetAllSchoolsAsync().Result;
            return Ok(schools);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolById(string id)
        {
            var school = await _schoolService.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return NotFound(new { error = "There is no school with id: " + id });
            }
            return Ok(school);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> AddSchool(School school)
        {
            if (school == null)
            {
                return BadRequest(new { error = "School cannot be null" });
            }
            string temp = await _schoolService.AddSchoolAsync(school);
            switch (temp)
            {
                case "Ok":
                    return CreatedAtAction(nameof(GetSchoolById), new { id = school.Id }, school);
                case "Name existed":
                    return Conflict(temp);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, temp);
            }
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> UpdateSchool(School school)
        {
            School? current = await _schoolService.GetByIdAsync(school.Id.ToString());
            if (current == null)
                return NotFound();
            await _schoolService.UpdateSchoolAsync(school);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.Staff)]
        public async Task<IActionResult> DeleteSchool(string id)
        {
            School? school = await _schoolService.GetByIdAsync(id);
            if (school == null)
                return NotFound();
            await _schoolService.DeleteSchoolAsync(id);
            return NoContent();
        }
    }
}
