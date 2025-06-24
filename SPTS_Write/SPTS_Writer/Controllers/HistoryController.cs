using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Services;

namespace SPTS_Writer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService historyService)
        {
            _historyService = historyService;
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
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> AddHistory(History history)
        {
            if (history == null)
            {
                return BadRequest("History cannot be null");
            }
            await _historyService.AddHistoryAsync(history);
            return CreatedAtAction(nameof(GetHistoryById), new { id = history.Id }, history);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitTest(TestSubmission submission)
        {
            if (submission.answers.Count == 0)
                return BadRequest("answers cannot be empty");
            History history = await _historyService.RecordTakenTestAsync(submission.test, submission.who, submission.answers);
            return CreatedAtAction(nameof(GetHistoryById), new { id = history.Id }, history);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id cannot be null or empty");
            }
            await _historyService.DeleteHistoryAsync(id);
            return NoContent();
        }

    }

}
