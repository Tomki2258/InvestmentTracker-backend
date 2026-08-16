using Microsoft.AspNetCore.Mvc;

namespace InvestmentTracker_backend.Controllers;

public class TestController : ControllerBase
{
    [HttpGet("TestGet")]
    public async Task<IActionResult> TestGet()
    {
        return Ok(new { message = "Test get" });
    }
}