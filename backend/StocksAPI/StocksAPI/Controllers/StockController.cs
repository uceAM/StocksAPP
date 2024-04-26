using Microsoft.AspNetCore.Mvc;
using StocksAPI.Dto.StockDto;
using StocksAPI.Interfaces;
using StocksAPI.Mapper;

namespace StocksAPI.Controllers;

[Route("api/stocks")]
[ApiController]
public class StockController : ControllerBase
{

    private readonly IStockService _stockService;
    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStocks()
    {
        var _ = await _stockService.GetAllStock();
        var data = _.Select(a => a.ToStockDto());
        return Ok(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var data = await _stockService.GetStock(id);
        if (data == null)
        {
            return NotFound();
        }
        return Ok(data.ToStockDto());
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateStocks([FromBody] CreateStockDto newData)
    {
        if (newData == null)
        {
            return BadRequest();
        }
        var model = newData.ToStock();
        if(await _stockService.AddStock(model))
            return CreatedAtAction(nameof(GetStockById), new { model.Id }, model.ToStockDto());
        return BadRequest();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(statusCode: 204)]
    [ProducesResponseType(statusCode: 400)]
    public async Task<IActionResult> UpdateStocks([FromRoute] int id, [FromBody] CreateStockDto updateData)
    {
        var dbStock = await _stockService.GetStock(id);
        if (dbStock == null)
        {
            return BadRequest();
        }
        if(await _stockService.UpdateStock(dbStock, updateData))
            return NoContent();
        return BadRequest();
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteStockById([FromRoute] int id)
    {
        var toDelete = await _stockService.GetStock(id);
        if (toDelete == null)
            return NotFound();
        await _stockService.RemoveStock(toDelete);
        return Ok(toDelete.ToStockDto());
    }
}
