using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker_backend.Models;

public class Portfolio
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
}