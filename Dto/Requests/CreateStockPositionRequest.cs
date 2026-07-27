using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker_backend.Dtos;

public class CreateStockPositionRequest
{
    [Required]
    public int StockId { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Quantity { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal PurchasePrice {get; set; }
}