using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTracker_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class StockPriceHistoryController(StockPriceHistoryService stockPriceHistoryService) : ControllerBase
{
    private readonly StockPriceHistoryService _stockPriceHistoryService = stockPriceHistoryService;

    [Authorize]
    [HttpGet("history")]
    public async Task<ActionResult<List<StockPriceHistory>>> GetStockPriceHistory(string ticker, DateTime startDate, DateTime endDate)
    {
        return await _stockPriceHistoryService.GetStockHistory(ticker,startDate, endDate);
    }
}