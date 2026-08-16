using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;
using InvestmentTracker_backend.Results;

namespace InvestmentTracker_backend.Services;

public class UserService(UsersRepository usersRepository)
{
    private readonly UsersRepository  _usersRepository = usersRepository;

    public async Task<User?> GetUserById(int id)
    {
        return await _usersRepository.GetUserById(id);
    }

    public async Task<int> Login(string email, string password)
    {
        var user = await _usersRepository.GetUserByEmail(email);
        if (user == null)
            return -1;
        var unHashedPassword = BCrypt.Net.BCrypt.Verify(password,user.Password);
        if (unHashedPassword)
        {
            return user.Id;
        }
        return -1;
    }

    public async Task<UserDto?> GetUserByEmail(string email)
    {
        var user = await _usersRepository.GetUserByEmail(email);
        var userDto = user?.GetDto();
        return userDto;
    }

    public async Task<bool> AddUser(string name,string surname, string email, string password)
    {
        var userCheck = await GetUserByEmail(email);
        if (userCheck != null)
            return false;
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User
        {
            Name = name,
            Surname = surname,
            Email = email,
            Password = hash
        };
        await _usersRepository.AddUser(user);
        return true;
    }   
}