using System.ComponentModel.DataAnnotations;
using InvestmentTracker_backend.Results;

namespace InvestmentTracker_backend.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Surname { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;

    public UserDto GetDto()
    {
        var dto = new UserDto()
        {
            Name = Name,
            Surname = Surname,
            Email = Email,
        };
        return dto;
    }
}