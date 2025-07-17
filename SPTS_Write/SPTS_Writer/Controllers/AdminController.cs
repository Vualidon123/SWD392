using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus.ViewChanges;
using SPTS_Writer.Services.Abstraction;
using System.Security.Claims;

namespace SPTS_Writer.Controllers
{
	[ApiController]
	[Route("api/admin")]
	public class AdminController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IQuestionService _questionService;
		private readonly ITestService _testService;
		private readonly INotificationService _notificationService;
		private readonly UserView _userView;

		public AdminController(
			IUserService userService,
			IQuestionService questionService,
			ITestService testService,
			INotificationService notificationService,
			UserView userView)
		{
			_userService = userService;
			_questionService = questionService;
			_testService = testService;
			_notificationService = notificationService;
			_userView = userView;
		}

		// ✅ Xóa tài khoản học sinh
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

		// ✅ Cập nhật role cho học sinh
		[HttpPut("students/{id}/role")]
		public async Task<IActionResult> UpdateRole(Guid id, [FromBody] string role)
		{
			var updated = await _userService.UpdateRoleAsync(id, role);
			return updated ? Ok(new { message = "Role updated successfully" }) : NotFound(new { message = "User not found" });
		}

		// ✅ Thống kê dữ liệu hệ thống
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

		// 🟢 Gửi thông báo cho 1 user
		[HttpPost("notifications/{userId}")]
		public async Task<IActionResult> SendNotificationToUser(Guid userId, [FromBody] string message)
		{
			// Kiểm tra user tồn tại không
			var user = await _userService.GetUserByIdAsync(userId.ToString());
			if (user == null)
			{
				return NotFound(new { message = "User not found" });
			}

			var notification = new Notification
			{
				UserId = userId, // 🟢 Gắn userId vào thông báo
				Message = message,
				CreatedAt = DateTime.UtcNow
			};

			await _notificationService.AddNotificationAsync(notification);

			return Ok(new
			{
				message = "Notification sent successfully to user",
				notification
			});
		}

		// 🟢 Lấy danh sách thông báo của 1 user
		[HttpGet("notifications/{userId}")]
		public async Task<IActionResult> GetNotificationsByUser(Guid userId)
		{
			var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
			return Ok(notifications);
		}

		// 🟢 API cũ - Broadcast cho tất cả users
		[HttpPost("notifications")]
		public async Task<IActionResult> SendBroadcastNotification([FromBody] Notification notification)
		{
			notification.CreatedAt = DateTime.UtcNow;
			await _notificationService.AddNotificationAsync(notification);

			return Ok(new
			{
				message = "Broadcast notification sent successfully",
				notification
			});
		}

		[HttpGet("notifications")]
		public async Task<IActionResult> GetAllNotifications()
		{
			var notifications = await _notificationService.GetAllNotificationsAsync();
			return Ok(notifications);
		}

		[HttpGet("me")]
		[Authorize] // 🛡 Yêu cầu JWT token để lấy userId
		public async Task<IActionResult> GetMyNotifications()
		{
			try
			{
				var userIdStr = GetCurrentUserId();
				if (string.IsNullOrEmpty(userIdStr))
				{
					return Unauthorized(new { message = "UserId not found in token" });
				}

				Guid userId = Guid.Parse(userIdStr);

				var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
				return Ok(notifications);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
		private string? GetCurrentUserId()
		{
			// Thử lấy bằng ClaimTypes.NameIdentifier
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!string.IsNullOrEmpty(userId))
			{
				return userId;
			}

			// Nếu không có, fallback sang claim "id" (nếu token đang chứa "id")
			userId = User.FindFirstValue("id");
			return userId;
		}
	}
}
