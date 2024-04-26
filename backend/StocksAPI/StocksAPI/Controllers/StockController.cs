using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksAPI.Data;
using StocksAPI.Dto.StockDto;
using StocksAPI.Mapper;

namespace StocksAPI.Controllers;

[Route("api/stocks")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStocks()
    {
        var _ = await _context.Stock.ToListAsync();
        var data = _.Select(a => a.ToStockDto());

        return Ok(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult >GetStockById([FromRoute] int id)
    {
        var data = await _context.Stock.FindAsync(id);
        if (data == null)
        {
            return NotFound();
        }
        return Ok(data.ToStockDto());
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task <IActionResult> CreateStocks([FromBody] CreateStockDto newData)
    {
        if (newData == null)
        {
            return BadRequest();
        }
        var model = newData.ToStock();
        await _context.Stock.AddAsync(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStockById), new { model.Id }, model.ToStockDto());
    }

    [HttpPut("{id}")]
    [ProducesResponseType(statusCode: 204)]
    [ProducesResponseType(statusCode: 400)]
    public async Task <IActionResult> UpdateStocks([FromRoute] int id, [FromBody] CreateStockDto updateData)
    {
        var dbStock = _context.Stock.Find(id);
        if (dbStock == null)
        {
            return BadRequest();
        }
        dbStock.Symbol = updateData.Symbol;
        dbStock.CompanyName = updateData.CompanyName;
        dbStock.MarketCap = updateData.MarketCap;
        dbStock.Purchase = updateData.Purchase;
        dbStock.Industry = updateData.Industry;
        dbStock.Dividend = updateData.Dividend;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task <IActionResult> DeleteStockById([FromRoute]int id)
    {
        var toDelete = _context.Stock.Find(id);
        if (toDelete == null)
            return NotFound();
        _context.Remove(toDelete);
        await _context.SaveChangesAsync();
        return Ok(toDelete.ToStockDto());
    }
}
