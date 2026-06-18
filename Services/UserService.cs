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
}