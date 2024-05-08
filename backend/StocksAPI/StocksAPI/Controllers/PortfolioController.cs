using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StocksAPI.Extensions;
using StocksAPI.Interfaces;
using StocksAPI.Models;

namespace StocksAPI.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<WebUser> _userManager;
    private readonly IPortfolioService _portfolioService;
    private readonly IStockService _stockService;
    public PortfolioController(UserManager<WebUser> userManager, IPortfolioService portfolioService, IStockService stockService)
    {
        _userManager = userManager;
        _portfolioService = portfolioService;
        _stockService = stockService;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        string? username = User.GetUsername();
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized("Unregistered User");
        }
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var userPortfolio = await _portfolioService.GetPortfolio(user);
        return Ok(userPortfolio);
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePortfolio([FromBody] string Symbol)
    {
        string? username = User.GetUsername();
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized("Unregistered User");
        }
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var stock = await _stockService.GetStockBySymbol(Symbol);
        if (stock == null)
        {
            return BadRequest("Stock not found");
        }
        var userPortfolio = await _portfolioService.GetPortfolio(user);
        if(userPortfolio.Any(x =>x.Symbol.ToLower() == Symbol.ToLower()))
        {
            return BadRequest($"User portfolio already contains {Symbol.ToUpper()}");
        }
        var portfolio = new Portfolio()
        {
            UserId = user.Id,
            StockId = stock.Id,
        };
        var CreatedPortfolio = await _portfolioService.AddPortfolio(portfolio);

        return StatusCode(201);
    }
}
