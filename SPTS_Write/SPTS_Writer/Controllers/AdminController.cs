using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Services;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok(new { message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                // Log exception (nếu cần)
                return BadRequest(new { message = $"Failed to delete user: {ex.Message}" });
            }
        }

        [HttpPut("students/{id}/role")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] string role)
        {
            var updated = await _userService.UpdateRoleAsync(id, role);
            return updated ? Ok() : NotFound();
        }

        // Thống kê dữ liệu hệ thống
        [HttpGet("reports")]
        public async Task<IActionResult> GetReport()
        {
            var totalUsers = await _userService.CountAsync();
            var totalQuestions = await _questionService.CountAsync();
            var totalTests = await _testService.CountAsync();

            return Ok(new
            {
                TotalUsers = totalUsers,
                TotalQuestions = totalQuestions,
                TotalTests = totalTests
            });
        }



    }

}
