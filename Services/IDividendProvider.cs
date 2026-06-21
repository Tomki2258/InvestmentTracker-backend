namespace InvestmentTracker_backend.Services;

public interface IDividendProvider
{
    public Task<decimal> GetDividend(string ticker);
}