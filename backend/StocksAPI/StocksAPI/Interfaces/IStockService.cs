using StocksAPI.Dto.StockDto;
using StocksAPI.Models;

namespace StocksAPI.Interfaces;

public interface IStockService
{
    public Task<List<Stock>> GetAllStock();
    public Task<Stock?> GetStock(int id);
    public Task<bool> AddStock(Stock stock);
    public Task<Stock> RemoveStock(Stock stock);
    public Task<bool> UpdateStock(Stock stock, CreateStockDto userStock);
    public Task<bool> StockExists(int id);
}
