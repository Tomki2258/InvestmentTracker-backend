namespace InvestmentTracker_backend.Dtos;

public class CreateStockPositionDto
{
    public int StockId { get; set; }
    public decimal Quantity { get; set; }
}