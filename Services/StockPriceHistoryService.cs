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
            Console.WriteLine("Empty stock price history");
            history =  await GetStockHistoryBySpan(ticker, startDate, endDate);
            await _repository.AddStockHistory(history);
        }
        Console.WriteLine($"First data {history[0].Date}");
        if (history[0].Date > startDate)
        {
            var missingHistory = await GetStockHistoryBySpan(ticker, startDate, history[0].Date.AddDays(-1));
            history.AddRange(missingHistory);
            await _repository.AddStockHistory(missingHistory);
        }
        history.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
        return history;
    }

    private async Task<List<StockPriceHistory>> GetStockHistoryBySpan(string ticker, DateTime startDate, DateTime endDate)
    {
        var historyApi = await Yahoo.GetHistoricalAsync(ticker, startDate, endDate, Period.Daily);
        var history = new List<StockPriceHistory>();
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
        return history;
    }
}