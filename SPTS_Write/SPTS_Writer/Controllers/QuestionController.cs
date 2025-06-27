using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Services;

namespace SPTS_Writer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public IActionResult GetQuestion()
        {
            var question = _questionService.GetAllQuestionsAsync().Result;
            return Ok(question);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            if (question == null)
            {
                return BadRequest("Question cannot be null");
            }
            await _questionService.AddQuestionAsync(question);
            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id cannot be null or empty");
            }
            await _questionService.DeleteQuestionAsync(id);
            return NoContent();
        }

    }

}

