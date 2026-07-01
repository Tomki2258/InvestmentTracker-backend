using InvestmentTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend.Repositories;

public class PortfolioRepository(ApiContext apiContext)
{
    private readonly ApiContext _apiContext = apiContext;

    public async Task<List<Portfolio>> GetAll(int userId)
    {
        return await _apiContext.portfolios.Where(p=>p.UserId==userId).ToListAsync();
    }

    public async Task<Portfolio> NewPortfolio(Portfolio portfolio)
    {
        await _apiContext.portfolios.AddAsync(portfolio);
        await _apiContext.SaveChangesAsync();
        return portfolio;
    }
    public async  Task<Portfolio> GetById(int portfolioId)
    {
        var portfolio = await _apiContext.portfolios.FirstOrDefaultAsync(p => p.Id == portfolioId);
        return portfolio;
    }
}