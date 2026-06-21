using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend.Repositories;

public class UsersRepository(ApiContext apiContext)
{
    private readonly ApiContext _apiContext = apiContext;
    public async Task<User?> GetUserById(int id)
    {
        return await apiContext.users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> IsUserValid(string email, string password)
    {
        var user = await apiContext.users.FirstOrDefaultAsync(u => u.Email.Equals(email));

        if (password.Equals(user.Password))
        {
            return user;
        }
        return null;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await apiContext.users.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }
}