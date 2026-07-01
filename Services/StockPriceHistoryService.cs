using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;
using YahooFinanceApi;

namespace InvestmentTracker_backend.Services;

public class StockPriceHistoryService(StockPriceHistoryRepository repository)
{
    private readonly StockPriceHistoryRepository _repository = repository;
    public async Task<List<StockPriceHistory>> GetStockHistory(string ticker, DateTime startDate, DateTime endDate)
    {
        var history = await _repository.GetStockHistory(ticker, startDate, endDate);
        if (history.Count == 0)
        {
            var historyApi = await Yahoo.GetHistoricalAsync(ticker, DateTime.Today.AddDays(-5), DateTime.Today, Period.Daily);
            foreach (var candle in historyApi)
            {
                var date = new DateTime(candle.DateTime.Year, candle.DateTime.Month, candle.DateTime.Day, 0, 0, 0, DateTimeKind.Utc);            
                var newHistory = new StockPriceHistory
                {   
                    Ticker = ticker,
                    Date = date,
                    Price = candle.AdjustedClose,
                };
                history.Add(newHistory);
            }
            await _repository.AddStockHistory(history);
        }
        return history;
    }
}