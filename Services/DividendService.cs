using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;

namespace InvestmentTracker_backend.Services;

public class DividendService(DividendRepository repository, StockService stockService)
{
    private readonly DividendRepository repository;
    private readonly StockService _stockService = stockService;
    public async Task<List<Dividend>> GetDividends(string ticker)
    {
        var stock = await _stockService.GetStockByTicker(ticker);

        if (stock != null)
        {
            Console.WriteLine($"EMPTY STOCK {ticker}");
            return null;
        }

        var dividends = await repository.GetDividendsByStock(stock.Id);

        return dividends;
    }
}