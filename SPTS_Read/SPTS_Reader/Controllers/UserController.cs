using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Entities;
using SPTS_Reader.Services;

namespace SPTS_Reader.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // 🟢 Lấy tất cả User - Không yêu cầu token
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    // 🟢 Lấy thông tin 1 User theo Id - Không yêu cầu token
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id.ToString());
        if (user == null)
            return NotFound(new { message = "User not found." });

        return Ok(user);
    }

    // 🟢 Lấy thông tin chính User - Không yêu cầu token
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        // ⚠️ Vì không có token nên không có cách xác định user
        return BadRequest(new { message = "Token is required to get current user." });
    }
}
