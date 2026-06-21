using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Results;

namespace InvestmentTracker_backend.Services.Interfaces;

public interface IStockPositionsService
{
    public Task<decimal> GetStockPositionAvg(string  ticker, int userId);
    public Task<List<StockPosition>> GetPositionsByStock(string ticker, int userId);

    public Task<StockProfitResult> GetProfit(string ticker, int userId);
    public Task<StockPosition> AddStockPosition(int stockId, decimal quantity, decimal purchasePrice, int userId);
    public Task<StockPosition> GetStockPositionById(int id);

}