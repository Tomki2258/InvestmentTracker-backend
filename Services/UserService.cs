using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;

namespace InvestmentTracker_backend.Services;

public class UserService(UsersRepository usersRepository)
{
    private readonly UsersRepository  _usersRepository = usersRepository;

    public async Task<User?> GetUserById(int id)
    {
        Console.WriteLine($"Szuka po id {id}");
        return await _usersRepository.GetUserById(id);
    }

    public async Task<int> IsValid(string email, string password)
    {
        var user = await _usersRepository.IsUserValid(email, password);
        if (user != null)
        {
            return user.Id;
        }

        return -1;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _usersRepository.GetUserByEmail(email);
        return user;
    }
}