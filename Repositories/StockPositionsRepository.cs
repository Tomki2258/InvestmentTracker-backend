using InvestmentTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend.Repositories;

public class StockPositionsRepository(ApiContext apiContext)
{
    private readonly ApiContext _apiContext = apiContext;

    public async Task<StockPosition> GetStockPositionById(int id)
    {
        var stockPosition = await _apiContext.stockPositions.FirstOrDefaultAsync(s => s.Id == id);
        return stockPosition;
    }

    public async Task<StockPosition> AddStockPosition(StockPosition stockPosition)
    {
        await _apiContext.AddAsync(stockPosition);
        await _apiContext.SaveChangesAsync();
        
        return  stockPosition;
    }

    public async Task<List<StockPosition>> GetPositionsByTicker(string ticker,int userId)
    {
        var stocks = await _apiContext.stockPositions
            .Where(s => s.Stock.Ticker == ticker)
            .Where(s => s.UserId == userId)
            .ToListAsync();
        return stocks;
    }
}