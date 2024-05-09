using Microsoft.EntityFrameworkCore;
using StocksAPI.Data;
using StocksAPI.Interfaces;
using StocksAPI.Models;

namespace StocksAPI.Repositories;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _context;
    public PortfolioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Portfolio> AddPortfolio(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }

    public async Task<List<Stock>> GetPortfolio(WebUser user)
    {
        return await _context.Portfolios.Where(x => x.UserId == user.Id)
            .Select(stock => new Stock()
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                Dividend = stock.Stock.Dividend,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap,
            }).ToListAsync();
    }

    public async Task<Portfolio>? RemovePortfolio(string userId, string Symbol)
    {
        var toRemove = _context.Portfolios.FirstOrDefault(x=>x.UserId == userId && x.Stock.Symbol == Symbol);
        if (toRemove == null)
        {
            return null;
        }
        _context.Portfolios.Remove(toRemove);
        await _context.SaveChangesAsync();
        return toRemove;

    }
}
