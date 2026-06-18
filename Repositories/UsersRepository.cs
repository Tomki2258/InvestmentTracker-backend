using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend.Repositories;

public class UsersRepository(ApiContext apiContext)
{
    public async Task<User?> GetUserById(int id)
    {
        return await apiContext.users.FirstOrDefaultAsync(u => u.Id == id);
    }
}