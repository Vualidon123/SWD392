using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPTS_Reader.Entities;
using SPTS_Reader.Services;

namespace SPTS_Reader.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly TestService _testService;

    public TestController(TestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTests(int limit, int skip)
    {
        var test = await _testService.GetBatchAsync(limit, skip);
        return Ok(test);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestById(Guid id)
    {
        var test = await _testService.GetByIdAsync(id);
        if (test == null)
        {
            return NotFound(new { error = "There is no test with id: " + id });
        }
        return Ok(test);
    }

    [HttpGet("by/{id}")]
    public async Task<IActionResult> GetTestsByAuthor(string id)
    {
        var test = await _testService.GetByAuthorIdAsync(id);
        return Ok(test);
    }

    [HttpGet("method/{method}")]
    public async Task<IActionResult> GetTestsByMethod(TestMethod method, int limit, int skip)
    {
        var test = await _testService.GetByTestMethodAsync(method, limit, skip);
        return Ok(test);
    }

}
