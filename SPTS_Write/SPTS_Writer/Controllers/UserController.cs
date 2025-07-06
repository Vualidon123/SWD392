using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Services.Abstraction;
using System.Security.Claims;

namespace SPTS_Writer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // 🟢 Create User - Chỉ Admin có quyền tạo User
        [HttpPost("create")]
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            user.CreatedAt = DateTime.UtcNow;


            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreatedAt = DateTime.UtcNow;
            await _userService.AddUserAsync(user);
            return Ok(new { message = "User created successfully." });
        }

        // 🟠 Update User - Cho phép chính User hoặc Admin chỉnh sửa
        [HttpPut("update/{id}")]
        [Authorize(Policy = AuthorizationPolicies.Student)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updatedUser)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            // ✅ Chỉ cho phép chính user hoặc admin update
            if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
                return Forbid();

            // ✅ Không đổi Id
            var existingUser = await _userService.GetUserByIdAsync(id.ToString());
            if (existingUser == null)
                return NotFound(new { message = "User not found." });

            // ✅ Update các field (trừ Id và Password nếu không đổi)
            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;
            existingUser.Role = updatedUser.Role ?? existingUser.Role; // giữ nguyên role nếu không truyền
            existingUser.UpdatedAt = DateTime.UtcNow;

            // ✅ Nếu đổi mật khẩu thì hash lại
            if (!string.IsNullOrWhiteSpace(updatedUser.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            }

            await _userService.UpdateUserAsync(existingUser);
            return Ok(new { message = "User updated successfully." });
        }


        // 🔴 Delete User - Admin xoá được mọi user, Student chỉ xoá chính mình
        [HttpDelete("delete/{id}")]
        [Authorize(Policy = AuthorizationPolicies.Student)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            // ✅ Nếu là Admin -> xoá được bất kỳ User
            if (User.IsInRole("Admin"))
            {
                await _userService.DeleteUserAsync(id.ToString());
                return Ok(new { message = "User deleted successfully by Admin." });
            }

            // ✅ Nếu là Student -> chỉ cho xoá chính mình
            if (currentUserId != id.ToString())
                return Forbid();

            await _userService.DeleteUserAsync(id.ToString());
            return Ok(new { message = "Your account has been deleted successfully." });
        }


        // Helper: Get current user Id from JWT claims
        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
