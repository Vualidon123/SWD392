using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus.Publishers;
using SPTS_Writer.Eventbus.ViewChanges;
using SPTS_Writer.Services;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IUserService _userService;

        public TestController(ITestService testService, IUserService userService)
        {
            _testService = testService;
            _userService = userService;

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
                return NotFound(new { error = "There's no test with id " + id });
            }

            return Ok(test);
        }

        [HttpGet("random")]
        [Authorize(Policy = AuthorizationPolicies.Student)]
        public async Task<IActionResult> CreateRandomTest(TestMethod method, int questionAmount)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Test test = await _testService.GenerateRandomTestAsync(method, questionAmount, userId);
            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> AddTest(Test test)
        {
            if (test == null)
            {
                return BadRequest(new { error = "Test cannot be null" });
            }
            await _testService.AddTestAsync(test);

            /*await _testView.CreateAllTestsViewAsync(); // Create the view for MBTI tests*/
            /*            await _testView.SyncTestSnapshotWithTestsAsync();*/ // Check and update the view if necessary

            return CreatedAtAction(nameof(GetTestById), new { id = test.Id }, test);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { error = "Id cannot be null or empty" });
            }
            await _testService.DeleteTestAsync(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(Test test)
        {
            if (string.IsNullOrEmpty(test.Id.ToString()))
            {
                return BadRequest(new { error = "Id cannot be null or empty" });
            }
            await _testService.UpdateTestAsync(test);
            return NoContent();
        }
    }
}
