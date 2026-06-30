using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;
using YahooFinanceApi;

namespace InvestmentTracker_backend.Services;

public class DividendService(DividendRepository repository, StockService stockService, IDividendProvider dividendProvider)
{
    private readonly DividendRepository _repository = repository;
    private readonly StockService _stockService = stockService;
    private readonly IDividendProvider dividendProvider = dividendProvider;
    public async Task<List<Dividend>> GetDividends(string ticker)
    {
        var stock = await _stockService.GetStockByTicker(ticker);
        
        if (stock == null)
        {
            Console.WriteLine($"Emtpy stock {ticker}");
            return null;
        }
        var dividends = await repository.GetDividendsByStock(stock.Id);
        if (dividends.Count == 0)
        {
            dividends = await dividendProvider.GetDividends(ticker);
            foreach (var dividend in dividends)
            {
                dividend.StockId = stock.Id;
                dividend.Stock = stock;
                await _repository.AddDividend(dividend);
            }   
        }
        return dividends;
    }
}