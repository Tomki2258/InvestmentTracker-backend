using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker_backend.Models;

public class Stock
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name  is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Ticker is required")]
    public string Ticker  { get; set; }
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Currency is required")]
    public string Currency { get; set; }
}