namespace InvestmentTracker_backend.Dtos;
public class GetStockPriceHistoryRequest
{
    public string Ticker { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}