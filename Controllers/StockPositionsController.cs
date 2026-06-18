using InvestmentTracker_backend.Dtos;
using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTracker_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class StockPositionsController(StockPositionsService stockPositionsService)
{
    private readonly StockPositionsService  _stockPositionsService = stockPositionsService;

    [HttpGet]
    public async Task<ActionResult<StockPosition>> GetStockPositionById(int id)
    {
        var stockPosition = await _stockPositionsService.GetStockPositionById(id);
        return stockPosition;
    }

    [HttpPost]
    public async Task<ActionResult<StockPosition>> AddStockPosition([FromBody]CreateStockPositionDto dto)
    {
        int userId = 1;
        var stockPosition = await _stockPositionsService.AddStockPosition(dto.StockId, dto.Quantity,dto.PurchasePrice, userId);
        if (stockPosition != null)
        {
            return stockPosition;
        }

        return null;
    }
}