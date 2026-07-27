using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;
using InvestmentTracker_backend.Results;
using InvestmentTracker_backend.Services.Interfaces;

namespace InvestmentTracker_backend.Services;

public class StockPositionsService(StockPositionsRepository stockPositionsRepository,StockService stockService,UserService userService) : IStockPositionsService
{
    private readonly StockPositionsRepository _stockPositionsRepository = stockPositionsRepository;
    private readonly StockService _stockService = stockService;
    private readonly UserService _userService = userService;
    public async Task<StockPosition> GetStockPositionById(int id)
    {
        var stockPosition = await _stockPositionsRepository.GetStockPositionById(id);
        return stockPosition;
    }

    public async Task<StockPosition> AddStockPosition(int stockId,decimal quantity,decimal purchasePrice,int userId)
    {
        var stock = await _stockService.GetStockById(stockId);
        var user = await userService.GetUserById(userId);

        if (stock == null || user == null)
        {
            return null;
        }
        var stockPositon = new StockPosition()
        {
            UserId = userId,
            StockId = stockId,
            Quantity = quantity,
            PurchasePrice = purchasePrice,
            PurchaseDate = DateTime.UtcNow,
            Stock = stock,
            User = user
        };
        await _stockPositionsRepository.AddStockPosition(stockPositon);
        return stockPositon;
    }

    public async Task<List<StockPosition>> GetPositionsByStock(string ticker,int userId)
    {
        var stocks = await _stockPositionsRepository.GetPositionsByTicker(ticker,userId);
        return stocks;
    }

    public async Task<decimal> GetStockPositionAvg(string ticker, int userId)
    {
        var stocks = await _stockPositionsRepository.GetPositionsByTicker(ticker, userId);
        return GetStockPositionAvg(stocks);
    }

    public decimal GetStockPositionAvg(List<StockPosition> stocks)
    {
        if (stocks == null || !stocks.Any())
        {
            return 0;
        }
        var totalQuantity = stocks.Sum(s => s.Quantity);
        if (totalQuantity == 0) 
        {
            return 0;
        }

        var totalCost = stocks.Sum(s => s.Quantity * s.PurchasePrice);
    
        return totalCost / totalQuantity;
    }
    public async Task<StockProfitResponse> GetProfit(string ticker, int userId)
    {
        var stocks = await _stockPositionsRepository.GetPositionsByTicker(ticker,userId);
        var avgPrice = GetStockPositionAvg(stocks);
        var currentPrice = await _stockService.GetStockPrice(ticker);

        var stocksCount = stocks.Sum(s => s.Quantity);
        var profit = Math.Round(currentPrice * stocksCount - avgPrice * stocksCount,2);
        var profitPercent = Math.Round((currentPrice - avgPrice) / avgPrice * 100,2);
        
        return new StockProfitResponse()
        {
            profit =  profit,
            profitPercent = profitPercent
        };
    }
}