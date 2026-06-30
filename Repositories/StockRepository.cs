using InvestmentTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend.Repositories;

public class StockRepository
{
    private readonly ApiContext apiContext;

    public StockRepository(ApiContext apiContext)
    {
        this.apiContext = apiContext;
    }

    public async Task<List<Stock>> GetStocks()
    {
        return await apiContext.stocks.ToListAsync(); 
    }

    public async Task<Stock> GetStockById(int id)
    {
        return  await apiContext.stocks.FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<Stock> GetStockByTicker(string ticker)
    {
        return await apiContext.stocks
            .FirstOrDefaultAsync(s => s.Ticker == ticker);
    }

    public async Task<bool> SetStock(Stock stock)
    {
        apiContext.stocks.Add(stock);
        var result = await apiContext.SaveChangesAsync() > 0;
        return result;
    }
}