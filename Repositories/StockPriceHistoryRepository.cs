using InvestmentTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend.Repositories;

public class StockPriceHistoryRepository(ApiContext apiContext)
{
    private readonly ApiContext _apiContext = apiContext;

    public async Task<List<StockPriceHistory>> GetStockHistory(string ticker, DateTime startDate, DateTime endDate)
    {
        var history = await _apiContext.stockPriceHistories
            .Where(s => s.Ticker == ticker && s.Date > startDate.AddDays(-1) && s.Date < endDate).ToListAsync();
        history.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
        return history;
    }

    public async Task<bool> AddStockHistory(List<StockPriceHistory> stockHistory)
    {
        await _apiContext.AddRangeAsync(stockHistory);
        await  _apiContext.SaveChangesAsync();
        return true;
    }
}