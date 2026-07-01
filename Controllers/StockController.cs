using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;
using InvestmentTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YahooFinanceApi;
using Microsoft.AspNetCore.Mvc;
namespace InvestmentTracker_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController(StockService stockService) : ControllerBase
{
    [Authorize]
    [HttpGet("price/{ticker}")]
    public double GetPriceByStock(string ticker)
    {
        var securities = Yahoo.Symbols(ticker).Fields(Field.Symbol, Field.RegularMarketPrice, Field.FiftyTwoWeekHigh).QueryAsync();
        var aapl = securities.Result[ticker];
        var price = aapl[
            Field.RegularMarketPrice];
        return price;
    }
    [Authorize]
    [HttpGet("details/{ticker}")]
    public async Task<ActionResult<Stock>> GetStockByTicker(string ticker)
    {
        var stock = await stockService.GetStockByTicker(ticker);
        return Ok(stock);
    }
}