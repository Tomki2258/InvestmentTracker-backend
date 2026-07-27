using InvestmentTracker_backend.Dtos;
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
    public async Task<ActionResult<List<StockPriceHistory>>> GetStockPriceHistory([FromQuery] GetStockPriceHistoryRequest request)
    {
        return await _stockPriceHistoryService.GetStockHistory(request.Ticker,request.EndDate, request.StartDate);
    }
}