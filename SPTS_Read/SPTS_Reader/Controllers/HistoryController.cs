using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Entities;
using SPTS_Reader.Services;
using System.Security.Claims;

namespace SPTS_Reader.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly HistoryService _historyService;

    public HistoryController(HistoryService historyService)
    {
        _historyService = historyService;
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.Staff)]
    public async Task<IActionResult> GetHistoriess(int limit, int skip)
    {
        var history = await _historyService.GetBatchAsync(limit, skip);
        return Ok(history);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Student)]
    public async Task<IActionResult> GetHistoryById(string id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var history = await _historyService.GetByIdAsync(Guid.Parse(id));
        if (history == null)
        {
            return NotFound(new { error = "There is no history with id: " + id });
        }
        if (history.UserId != Guid.Parse(userId))
            return StatusCode(StatusCodes.Status403Forbidden, (new { error = "There is no history with id: " + id }));
        return Ok(history);
    }

    [HttpGet("student-history/{id}")]
    [Authorize(Policy = AuthorizationPolicies.Staff)]
    public async Task<IActionResult> GetStudentHistoryById(Guid id)
    {
        var history = await _historyService.GetByIdAsync(id);
        if (history == null)
        {
            return NotFound(new { error = "There is no history with id: " + id });
        }
        return Ok(history);
    }

    [HttpGet("user")]
    [Authorize(Policy = AuthorizationPolicies.Student)]
    public async Task<IActionResult> GetHistorysByUser(int limit, int skip)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var history = await _historyService.GetByUserIdAsync(Guid.Parse(userId), limit, skip);
        return Ok(history);
    }

    [HttpGet("user/{id}")]
    [Authorize(Policy = AuthorizationPolicies.Staff)]
    public async Task<IActionResult> GetHistorysByUser(Guid id, int limit, int skip)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var history = await _historyService.GetByUserIdAsync(id, limit, skip);
        return Ok(history);
    }
}
