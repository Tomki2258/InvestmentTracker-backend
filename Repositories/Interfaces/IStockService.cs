using InvestmentTracker_backend.Models;

namespace InvestmentTracker_backend.Services.Interfaces;

public interface IStockService
{
    public Task<Stock> GetStockByTicker(string ticker);
}