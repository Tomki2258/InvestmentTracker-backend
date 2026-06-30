using InvestmentTracker_backend.Models;
using YahooFinanceApi;

namespace InvestmentTracker_backend.Services;

public class YahooDividendProvider : IDividendProvider
{
    public async Task<List<Dividend>> GetDividends(string ticker)
    {
        Console.WriteLine($"Calling {this.GetType().Name} for dividends");
        var dividendsApi = await Yahoo.GetDividendsAsync(ticker, new DateTime(2016, 1, 1), DateTime.UtcNow);
        List<Dividend> dividends = new List<Dividend>();
        foreach (var candle in dividendsApi)
        {
            var date = new DateTime(candle.DateTime.Year, candle.DateTime.Month, candle.DateTime.Day, 0, 0, 0, DateTimeKind.Utc);            var newDividend = new Dividend
            {
                Amount = (decimal)candle.Dividend,
                Date = date,
            };
            dividends.Add(newDividend);
        }
        return dividends;
    }
}