using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;
using InvestmentTracker_backend.Results;
using InvestmentTracker_backend.Services.Interfaces;

namespace InvestmentTracker_backend.Services;

public class StockPositionsService(PortfolioService portfolioService,StockPositionsRepository stockPositionsRepository,StockService stockService,UserService userService) : IStockPositionsService
{
    private readonly StockPositionsRepository _stockPositionsRepository = stockPositionsRepository;
    private readonly StockService _stockService = stockService;
    private readonly PortfolioService _portfolioService = portfolioService;
    public async Task<StockPosition> GetStockPositionById(int id)
    {
        var stockPosition = await _stockPositionsRepository.GetStockPositionById(id);
        return stockPosition;
    }
    

    public async Task<StockPosition> AddStockPosition(int stockId,decimal quantity,decimal purchasePrice,int portfolioId)
    {
        var stock = await _stockService.GetStockById(stockId);
        var portfolio = await _portfolioService.GetPortfolioById(portfolioId);
        
        if (stock == null ||  portfolio == null)
        {
            return null;
        }
        var stockPositon = new StockPosition()
        {
            PortfolioId = portfolioId,
            StockId = stockId,
            Quantity = quantity,
            PurchasePrice = purchasePrice,
            PurchaseDate = DateTime.UtcNow,
            Stock = stock,
            portfolio = portfolio
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
    public async Task<StockProfitResult> GetProfit(string ticker, int userId)
    {
        var stocks = await _stockPositionsRepository.GetPositionsByTicker(ticker,userId);
        var profitResult = await GetStockProfitResult(stocks);
        return profitResult;
    }

    private async Task<StockProfitResult> GetStockProfitResult(List<StockPosition> stocks)
    {
        var avgPrice = GetStockPositionAvg(stocks);
        var currentPrice = await _stockService.GetStockPrice(stocks[0].Stock.Ticker);

        var stocksCount = stocks.Sum(s => s.Quantity);
        var profit = Math.Round(currentPrice * stocksCount - avgPrice * stocksCount,2);
        var profitPercent = Math.Round((currentPrice - avgPrice) / avgPrice * 100,2);
        
        return new StockProfitResult()
        {
            ticker = stocks[0].Stock.Ticker,
            profit =  profit,
            profitPercent = profitPercent
        };
    }
    public async Task<List<StockPosition>> GetPositionsByPortfolio(int portfolioId)
    {
        return await _stockPositionsRepository.GetPositionsByPortfolio(portfolioId);
    }

    public async Task<decimal> GetPortolioProfit(int userId, string portfolioName)
    {
        var portfolios = await _portfolioService.GetAll(userId);
        var portfolio = portfolios.Find(s => s.Name == portfolioName);
        
        var stocks = await GetPositionsByPortfolio(portfolio.Id);
        var groupStocks = stocks.GroupBy(s=>s.Stock.Ticker);
        decimal sumProfit = 0;
        foreach (var stock in groupStocks)
        {
            var s = stock.ToList();
            var profit = await GetStockProfitResult(s);
            sumProfit += profit.profit;
        }
        return sumProfit;
    }
}