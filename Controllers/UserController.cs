using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Results;
using InvestmentTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTracker_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService  _userService = userService;

    [Authorize]
    [HttpGet("email")]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    } 
}