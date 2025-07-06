using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Services.Abstraction;
using System.Security.Claims;

namespace SPTS_Reader.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly IUserService _userService;

    public StaffController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> GetAllStaff()
    {
        var staffList = await _userService.GetUsersByRoleAsync("Staff");
        return Ok(staffList);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> GetStaffById(Guid id)
    {
        var staff = await _userService.GetUserByIdAsync(id.ToString());
        if (staff == null || staff.Role != "Staff")
            return NotFound(new { message = "Staff not found." });

        return Ok(staff);
    }

    [HttpGet("me")]
    [Authorize(Policy = AuthorizationPolicies.Staff)]
    public async Task<IActionResult> GetOwnProfile()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId == null)
            return Unauthorized();

        var staff = await _userService.GetUserByIdAsync(currentUserId);
        if (staff == null || staff.Role != "Staff")
            return Forbid();

        return Ok(staff);
    }
}
