using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvestmentTracker_backend.Dtos;
using InvestmentTracker_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InvestmentTracker_backend.Controllers;

public class AuthController(IConfiguration config, UserService userService) : ControllerBase
{
    private readonly IConfiguration _config = config;
    private readonly UserService _userService = userService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var userId = await _userService.IsValid(model.Email, model.Password);
        if (userId == -1)
        {
            return Unauthorized();
        }

        var secret = _config["JwtConfig:Secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, model.Email),
            new Claim(ClaimTypes.Role, "User")
        };

        var token = new JwtSecurityToken(
            issuer: _config["JwtConfig:Issuer"],
            audience: _config["JwtConfig:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    [HttpPost("register")]
    public async  Task<IActionResult> Register([FromBody] RegisterUserRequestDto model)
    {
        var r =await _userService.AddUser(model.Name, model.Surname, model.Email, model.Password);
        if (r)
        {
            return Ok();
        }
        return BadRequest("Failed to register user");
    }
}