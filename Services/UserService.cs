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

    public async Task<bool> AddUser(string name,string surname, string email, string password)
    {
        var userCheck = await GetUserByEmail(email);
        if (userCheck != null)
            return false;
        var user = new User
        {
            Name = name,
            Surname = surname,
            Email = email,
            Password = password
        };
        await _usersRepository.AddUser(user);
        return true;
    }   
}