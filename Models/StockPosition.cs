using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentTracker_backend.Models;

public class StockPosition
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PortfolioId { get; set; }
    
    [ForeignKey(nameof(PortfolioId))]
    public Portfolio portfolio { get; set; } = null!;
    [Required]
    public int StockId { get; set; }
    
    [ForeignKey(nameof(StockId))]
    public Stock Stock { get; set; } = null!;
    [Required]
    public decimal Quantity { get; set; }

    [Required]
    public decimal PurchasePrice { get; set; }

    [Required]
    public DateTime PurchaseDate { get; set; }
}