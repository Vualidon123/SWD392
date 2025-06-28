using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Entities;
using SPTS_Reader.Models;
using SPTS_Reader.Service;

namespace SPTS_Reader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService _testService;
        
        public TestController(TestService testService)
        {
            _testService = testService;
           
        }
        [HttpGet]
        public IActionResult GetTest()
        {
            var test = _testService.GetAllTestsAsync().Result;
            return Ok(test);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(string id)
        {
            var test = await _testService.GetTestByIdAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> AddTest(Test test)
        {
            if (test == null)
            {
                return BadRequest("Test cannot be null");
            }
            await _testService.AddTestAsync(test);
            return CreatedAtAction(nameof(GetTestById), new { id = test.Id }, test);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id cannot be null or empty");
            }
            await _testService.DeleteTestAsync(id);
            return NoContent();
        }
    }

}
