using StocksAPI.Models;

namespace StocksAPI.Interfaces;

public interface IPortfolioService
{
    public Task<List<Stock>> GetPortfolio(WebUser user);
    public Task<Portfolio> AddPortfolio(Portfolio portfolio);
    public Task<Portfolio>? RemovePortfolio(string userId, string Symbol);
}
