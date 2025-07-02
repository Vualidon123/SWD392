using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;


        public AdminController(
           IUserService userService,
           IQuestionService questionService,
           ITestService testService)
        {
            _userService = userService;
            _questionService = questionService;
            _testService = testService;
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
