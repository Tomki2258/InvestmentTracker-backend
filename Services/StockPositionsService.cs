using InvestmentTracker_backend.Models;
using InvestmentTracker_backend.Repositories;

namespace InvestmentTracker_backend.Services;

public class StockPositionsService(StockPositionsRepository stockRepository,StockService stockService,UserService userService)
{
    private readonly StockPositionsRepository _stockRepository = stockRepository;
    private readonly StockService _stockService = stockService;
    private readonly UserService _userService = userService;
    public async Task<StockPosition> GetStockPositionById(int id)
    {
        var stockPosition = await _stockRepository.GetStockPositionById(id);
        return stockPosition;
    }

    public async Task<StockPosition> AddStockPosition(int stockId,decimal quantity,int userId)
    {
        var stock = await _stockService.GetStockById(stockId);
        var user = await userService.GetUserById(userId);

        if (stock == null || user == null)
        {
            return null;
        }
        var stockPositon = new StockPosition()
        {
            UserId = userId,
            StockId = stockId,
            Quantity = quantity,
            PurchasePrice = (decimal)stock.Price,
            PurchaseDate = DateTime.UtcNow,
            Stock = stock,
            User = user
        };
        await _stockRepository.AddStockPosition(stockPositon);
        return stockPositon;
    }
}