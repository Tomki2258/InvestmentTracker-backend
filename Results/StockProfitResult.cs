namespace InvestmentTracker_backend.Results;

public class StockProfitResult
{
    public string ticker { get; set; }
    public decimal profit { get; set; }
    public decimal profitPercent { get; set; }
}