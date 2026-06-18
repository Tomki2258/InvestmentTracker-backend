using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;
using Microsoft.Extensions.Logging;
using YahooFinanceApi;

namespace InvestmentTracker_backend.Services;

public class StockService(StockRepository repository, ILogger<StockService> logger)
{
    private readonly StockRepository _stockRepository = repository;
    private readonly ILogger<StockService> _logger = logger;

    public async Task<Stock?> GetStockById(int id)
    {
        var stock = await _stockRepository.GetStockById(id);
        return stock;
    }
    public async Task<Stock?> GetStockByTicker(string ticker)
    {
        var stock = await _stockRepository.GetStockByTicker(ticker);
        
        if (stock == null)
        {
            try
            {
                var securities = await Yahoo.Symbols(ticker)
                    .Fields(Field.Symbol, Field.LongName, Field.Currency)
                    .QueryAsync();

                if (securities == null || !securities.ContainsKey(ticker))
                {
                    return null;
                }

                var stockData = securities[ticker];
                var price = stockData[Field.RegularMarketPrice];
                var name = stockData[Field.LongName]?.ToString() ?? ticker;
                var currency = stockData[Field.Currency]?.ToString() ?? ticker;

                stock = new Stock()
                {
                    Name = name,
                    Ticker = ticker.ToUpper(),
                    Price = Convert.ToDecimal(price),   
                    Currency = currency
                };
                await SetStock(stock);
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        return stock;
    }

    public async Task<bool> SetStock(Stock stock)
    {
        var result = await _stockRepository.SetStock(stock);
        if (!result)
        {
            return false;
        }
        
        return result;
    }
}