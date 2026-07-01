using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTracker_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController(PortfolioService portfolioService) : ControllerBase
{
    private readonly PortfolioService _portfolioService = portfolioService;
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Portfolio>>> GetAll()
    {
        var userClam = User.FindFirst(System.Security.Claims.ClaimTypes.PrimarySid);
        var userId = Convert.ToInt32(userClam.Value);
        var portfolios = await _portfolioService.GetAll(userId);
        return Ok(portfolios);
    }
    [Authorize]
    [HttpPost("new")]
    public async Task<ActionResult<Portfolio>> New(string name)
    {
        var userClam = User.FindFirst(System.Security.Claims.ClaimTypes.PrimarySid);
        var userId = Convert.ToInt32(userClam.Value);

        var result = await _portfolioService.NewPortfolio(userId, name);
        if(result == null)
            return BadRequest();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("byId")]
    public async Task<ActionResult<Portfolio>> Get(int portfolioId)
    {
        var portfolio = await _portfolioService.GetPortfolioById(portfolioId);
        if(portfolio == null)
            return NotFound();
        return Ok(portfolio);
    }
}