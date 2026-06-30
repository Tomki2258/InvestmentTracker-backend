using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTracker_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class DividendController(DividendService dividendService) : ControllerBase
{
    private readonly DividendService dividendService = dividendService;

    [HttpGet]
    public async Task<ActionResult<List<Dividend>>> Get(string ticker)
    {
        var dividends = await dividendService.GetDividends(ticker);
        
        return Ok(dividends);
    }
}