using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Entities;
using SPTS_Reader.Services;

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
    public async Task<IActionResult> GetHistoriess(int limit, int skip)
    {
        var history = await _historyService.GetBatchAsync(limit, skip);
        return Ok(history);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHistoryById(Guid id)
    {
        var history = await _historyService.GetByIdAsync(id);
        if (history == null)
        {
            return NotFound(new { error = "There is no history with id: " + id });
        }
        return Ok(history);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetHistorysByMethod(Guid userId, int limit, int skip)
    {
        var history = await _historyService.GetByUserIdAsync(userId, limit, skip);
        return Ok(history);
    }

}

