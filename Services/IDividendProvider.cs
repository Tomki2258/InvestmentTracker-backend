using InvestmentTracker_backend.Models;

namespace InvestmentTracker_backend.Services;

public interface IDividendProvider
{
    public Task<List<Dividend>> GetDividends(string ticker);
}