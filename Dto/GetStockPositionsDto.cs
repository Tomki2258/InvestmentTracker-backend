using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker_backend.Dtos;

public class GetStockPositionsDto
{
    [Required] 
    public string Ticker { get; set; }
}