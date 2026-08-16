using System.Security.Claims;
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

    [Authorize]
    [HttpGet("current")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (email == null)
        {
            return Unauthorized();
        }
        var user = await _userService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound("Cannot read current user data");
        }

        var dto = user.ToDto();
        return Ok(dto);
    }
}