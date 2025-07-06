using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Services.Abstraction;
using BCrypt.Net;
using System.Security.Claims;

namespace SPTS_Writer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly IUserService _userService;

    public StaffController(IUserService userService)
    {
        _userService = userService;
    }

    // ✅ Admin: Create new Staff
    [HttpPost("create")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> CreateStaff([FromBody] User staff)
    {
        // Gán role mặc định cho staff
        staff.Role = "Staff";
        staff.Password = BCrypt.Net.BCrypt.HashPassword(staff.Password); // Hash password
        staff.CreatedAt = DateTime.UtcNow;

        await _userService.AddUserAsync(staff);
        return Ok(new { message = "Staff created successfully." });
    }

    // ✅ Admin: Update any Staff
    [HttpPut("update/{id}")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> UpdateStaff(Guid id, [FromBody] User updatedStaff)
    {
        updatedStaff.Id = id;
        updatedStaff.Role = "Staff"; // Ensure role remains Staff
        updatedStaff.UpdatedAt = DateTime.UtcNow;

        await _userService.UpdateUserAsync(updatedStaff);
        return Ok(new { message = "Staff updated successfully." });
    }

    // ✅ Admin: Delete any Staff
    [HttpDelete("delete/{id}")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> DeleteStaff(Guid id)
    {
        await _userService.DeleteUserAsync(id.ToString());
        return Ok(new { message = "Staff deleted successfully." });
    }

    // ✅ Staff: Update own profile
    [HttpPut("update-profile")]
    [Authorize(Policy = AuthorizationPolicies.Staff)]
    public async Task<IActionResult> UpdateOwnProfile([FromBody] User updatedStaff)
    {
        var currentUserId = GetCurrentUserId();
        if (currentUserId == null || currentUserId != updatedStaff.Id.ToString())
            return Unauthorized();

        updatedStaff.UpdatedAt = DateTime.UtcNow;
        updatedStaff.Role = "Staff"; // Role fixed
        updatedStaff.Password = BCrypt.Net.BCrypt.HashPassword(updatedStaff.Password); // hash lại nếu đổi password

        await _userService.UpdateUserAsync(updatedStaff);
        return Ok(new { message = "Profile updated successfully." });
    }

    // Helper: Get current logged-in userId
    private string? GetCurrentUserId()
    {
        return HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
