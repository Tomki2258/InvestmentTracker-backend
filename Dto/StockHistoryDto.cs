namespace InvestmentTracker_backend.Dtos;

public class StockHistoryDto
{
    public string Ticker { get; set; }
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
}