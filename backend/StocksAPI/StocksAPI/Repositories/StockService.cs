using Microsoft.EntityFrameworkCore;
using StocksAPI.Data;
using StocksAPI.Dto.StockDto;
using StocksAPI.Interfaces;
using StocksAPI.Models;

namespace StocksAPI.Repositories;

public class StockService : IStockService
{
    public readonly ApplicationDbContext _context;
    public StockService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddStock(Stock stock)
    {
        await _context.Stock.AddAsync(stock);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Stock>> GetAllStock()
    {
        var data = await _context.Stock.Include(c => c.Comments).ToListAsync();
        return data;
    }

    public async Task<Stock?> GetStock(int id)
    {
        var data = await _context.Stock.Include(c=>c.Comments).FirstOrDefaultAsync(c => c.Id == id);
        return data;
    }

    public async Task<Stock> RemoveStock(Stock model)
    {
        _context.Remove(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<bool> UpdateStock(Stock dbStock, CreateStockDto updateData)
    {
        dbStock.Symbol = updateData.Symbol;
        dbStock.CompanyName = updateData.CompanyName;
        dbStock.MarketCap = updateData.MarketCap;
        dbStock.Purchase = updateData.Purchase;
        dbStock.Industry = updateData.Industry;
        dbStock.Dividend = updateData.Dividend;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> StockExists(int id)
    {
        return await _context.Stock.AnyAsync(x => x.Id == id);
    }
}
