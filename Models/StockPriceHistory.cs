using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker_backend.Models;

public class StockPriceHistory
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Ticker { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public decimal Price { get; set; }
}