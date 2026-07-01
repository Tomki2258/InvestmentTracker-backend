using InvestmentTracker_backend.Dtos;
using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Results;
using InvestmentTracker_backend.Services;
using InvestmentTracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTracker_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class StockPositionsController(IStockPositionsService stockPositionsService) : ControllerBase
{
    private readonly IStockPositionsService _stockPositionsService = stockPositionsService;
    [Authorize]
    [HttpGet("positions")]
    public async Task<ActionResult<List<StockPosition>>> GetStockPositions([FromQuery] string? ticker)
    {
        int userId = 1;
        
        var stocks = await _stockPositionsService.GetPositionsByStock(ticker, userId);
        if (stocks.Count == 0)
        {
            return NotFound();
        }
        return Ok(stocks);
    }
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StockPosition>> GetStockPositionById(int id)
    {
        var stockPosition = await _stockPositionsService.GetStockPositionById(id);
        
        if (stockPosition == null)
        {
            return NotFound();
        }
        
        return Ok(stockPosition);
    }
    [Authorize]
    [HttpPost("add")] 
    public async Task<ActionResult<StockPosition>> AddStockPosition([FromBody] CreateStockPositionDto dto)
    {
        int userId = 1;
        var stockPosition = await _stockPositionsService.AddStockPosition(dto.StockId, dto.Quantity, dto.PurchasePrice, dto.PortfolioId);
    
        if (stockPosition != null)
        {
            return Ok(stockPosition);
        }

        return BadRequest("Failed to add stock position"); 
    }
    [Authorize]
    [HttpGet("avg")]
    public async Task<ActionResult<decimal>> AvgBuyPrice(string ticker)
    {
        int userId = 1;
        var avgPrice = await _stockPositionsService.GetStockPositionAvg(ticker, userId);
        return Ok(avgPrice);
    }
    [Authorize]
    [HttpGet("profit")]
    public async Task<ActionResult<StockProfitResult>> GetProfit(string ticker)
    {
        int userId = 1;
        var profit = await _stockPositionsService.GetProfit(ticker, userId);
        return Ok(profit);
    }
}