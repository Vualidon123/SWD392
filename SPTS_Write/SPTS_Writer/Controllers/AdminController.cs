﻿using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus.ViewChanges;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;
        private readonly UserView _userView;

        public AdminController(
            IUserService userService,
            IQuestionService questionService,
            ITestService testService,
            UserView userView)
        {
            _userService = userService;
            _questionService = questionService;
            _testService = testService;
            _userView = userView;
        }

        // Xóa tài khoản học sinh
        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                // Cập nhật lại view sau khi xóa người dùng
                /*await _userView.SyncUserSnapshotWithUsersAsync();*/
                return Ok(new { message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Failed to delete user: {ex.Message}" });
            }
        }

        // Cập nhật role cho học sinh
        [HttpPut("students/{id}/role")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] string role)
        {
            var updated = await _userService.UpdateRoleAsync(id, role);
            return updated ? Ok(new { message = "Role updated successfully" }) : NotFound(new { message = "User not found" });
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

        //[HttpPost("notifications")]
        //public async Task<IActionResult> SendNotification([FromBody] string message)
        //{
        //    var notification = new Notification { Message = message };
        //    await _db.Notifications.InsertOneAsync(notification);
        //    return Ok();
        //}

    }
}
