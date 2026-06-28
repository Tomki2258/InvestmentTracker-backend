using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker_backend.Dtos;

public class RegisterUserRequestDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty; 
    [Required]
    public string Password { get; set; } = string.Empty;
}