using InvestmentTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend.Repositories;

public class StockPriceHistoryRepository(ApiContext apiContext)
{
    private readonly ApiContext _apiContext = apiContext;

    public Task<List<StockPriceHistory>> GetStockHistory(string ticker, DateTime startDate, DateTime endDate)
    {
        return _apiContext.stockPriceHistories
            .Where(s => s.Ticker == ticker && s.Date > startDate && s.Date < endDate).ToListAsync();
    }

    public async Task<bool> AddStockHistory(List<StockPriceHistory> stockHistory)
    {
        await _apiContext.AddRangeAsync(stockHistory);
        await  _apiContext.SaveChangesAsync();
        return true;
    }
}