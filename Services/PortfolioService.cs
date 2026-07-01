using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;

namespace InvestmentTracker_backend.Services;

public class PortfolioService(PortfolioRepository portfolioRepository)
{
    private readonly PortfolioRepository _portfolioRepository = portfolioRepository;

    public async Task<List<Portfolio>> GetAll(int userId)
    {
        return await _portfolioRepository.GetAll(userId);
    }

    public async Task<Portfolio> GetPortfolioById(int id)
    {
        var portfolio = await _portfolioRepository.GetById(id);
        return portfolio;
    }
    public async Task<Portfolio> NewPortfolio(int userId, string name)
    {
        var newPortfolio = new Portfolio
        {
            Name = name,
            UserId = userId
        };
        var result = await _portfolioRepository.NewPortfolio(newPortfolio);
        return result;
    }
}