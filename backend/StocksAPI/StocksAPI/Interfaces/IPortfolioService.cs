using StocksAPI.Models;

namespace StocksAPI.Interfaces;

public interface IPortfolioService
{
    public Task<List<Stock>> GetPortfolio(WebUser user);
}
