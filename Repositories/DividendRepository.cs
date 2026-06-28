using InvestmentTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend.Repositories;

public class DividendRepository(ApiContext apiContext)
{
    private readonly ApiContext apiContext = apiContext;

    public async Task<List<Dividend>> GetDividendsByStock(int id)
    {
        var dividends = await apiContext.dividends.Where(div => div.Id == id).ToListAsync();
        
        return dividends;   
    }

    public async Task<Dividend> AddDividend(Dividend dividend)
    {
        await apiContext.dividends.AddAsync(dividend);
        await apiContext.SaveChangesAsync();
        return dividend;
    }
}