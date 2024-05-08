using Microsoft.EntityFrameworkCore;
using StocksAPI.Data;
using StocksAPI.Dto.StockDto;
using StocksAPI.Helpers;
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
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Stock>> GetAllStock(StockQueryObject query)
    {
        var PageCalc = query.PageNumber * query.PageSize;
        var data = _context.Stocks.Include(c => c.Comments).AsQueryable();
        // Filtering based on query params
        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            data = data.Where(s => s.Symbol.Contains(query.Symbol.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            data = data.Where(s => s.CompanyName.Contains(query.CompanyName.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                data = query.SortOrder ? data.OrderByDescending(s => s.Symbol) : data.OrderBy(s => s.Symbol);
            }
            else
            {
                data = query.SortOrder ? data.OrderByDescending(s => s.CompanyName) : data.OrderBy(s => s.CompanyName);
            }
        }
        else
        {
            data = query.SortOrder ? data.OrderByDescending(s => s.Id) : data.OrderBy(s => s.Id);
        }
        return await data.Skip(PageCalc).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetStock(int id)
    {
        var data = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(c => c.Id == id);
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
        return await _context.Stocks.AnyAsync(x => x.Id == id);
    }

    public async Task<Stock?> GetStockBySymbol(string symbol)
    {
        return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
    }
}
