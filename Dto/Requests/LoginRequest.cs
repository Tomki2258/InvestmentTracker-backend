using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker_backend.Dtos;

public class LoginRequest
{
    [Required]
    public string Email { get; set; } 
    [Required]
    public string Password { get; set; }
}