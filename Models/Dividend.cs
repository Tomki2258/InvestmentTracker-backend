using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker_backend.Models;

public class Dividend
{
    [Required]
    public int Id { get; set; } 
    [Required]
    public int StockId { get; set; }
    [Required]
    public Stock Stock { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public DateTime Date { get; set; }
}