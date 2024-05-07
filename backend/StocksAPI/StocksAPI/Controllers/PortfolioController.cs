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
    public PortfolioController(UserManager<WebUser> userManager, IPortfolioService portfolioService)
    {
        _userManager = userManager;
        _portfolioService = portfolioService;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        string? username = User.GetUsername();
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized("username not found");
        }
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var userPortfolio = await _portfolioService.GetPortfolio(user);
        return Ok(userPortfolio);
    }
}
