using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Services;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryService _historyService;
        private readonly ITestService _testService;
        private readonly UserService _userService;

        public HistoryController(HistoryService historyService, ITestService testService, UserService userService)
        {
            _historyService = historyService;
            _testService = testService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetHistory()
        {
            var history = _historyService.GetAllHistorysAsync().Result;
            return Ok(history);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryById(string id)
        {
            var history = await _historyService.GetHistoryByIdAsync(id);
            if (history == null)
            {
                return NotFound(new { error = "There is no history with id: " + id });
            }
            return Ok(history);
        }

        [HttpPost("submit")]
        [Authorize(Policy = AuthorizationPolicies.Student)]
        public async Task<IActionResult> SubmitTest([FromBody] TestSubmission submission, TestStatus status)
        {
            if (submission.answers.Count == 0)
                return BadRequest(new { error = "answers cannot be empty" });
            Test? test = await _testService.GetTestByIdAsync(submission.TestID);
            if (test == null)
                return BadRequest(new { error = "Cannot find test with this ID" });
            if (status == TestStatus.Completed && submission.answers.Count != test.NumberOfQuestions)
                return BadRequest(new { error = "Cannot complete a partial test, there're only " + submission.answers.Count + " question answered while the test has " + test.NumberOfQuestions + " questions" });
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            User? temp = await _userService.GetUserByIdAsync(userId);
            if (temp == null)
                return BadRequest(new { error = "Cannot get User information" });
            History history = await _historyService.RecordTakenTestAsync(test, temp, submission.answers, status);
            return CreatedAtAction(nameof(GetHistoryById), new { id = history.Id }, history);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { error = "Id cannot be null or empty" });
            }
            await _historyService.DeleteHistoryAsync(id);
            return NoContent();
        }

    }

}
