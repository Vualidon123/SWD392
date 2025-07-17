using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Services.Abstraction;
using System;
using System.Threading.Tasks;

namespace SPTS_Reader.Controllers
{
	[ApiController]
	[Route("api/notifications")]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		// ✅ Lấy thông báo của user hiện tại
		[HttpGet("me")]
		[Authorize] // 🛡 Yêu cầu JWT token để lấy userId
		public async Task<IActionResult> GetMyNotifications()
		{
			try
			{
				// Lấy userId từ JWT token
				var userIdClaim = User.FindFirst("id");
				if (userIdClaim == null)
				{
					return Unauthorized(new { message = "UserId not found in token" });
				}

				Guid userId = Guid.Parse(userIdClaim.Value);

				var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);

				return Ok(notifications);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
