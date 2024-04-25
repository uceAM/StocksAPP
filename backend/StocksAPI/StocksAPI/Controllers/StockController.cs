﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAllStocks()
    {
        var data = _context.Stock.Select(a => a.ToStockDto()).ToList();

        return Ok(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult GetStockById([FromRoute] int id)
    {
        var data = _context.Stock.Find(id);
        if (data == null)
        {
            return NotFound();
        }
        return Ok(data.ToStockDto());
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public IActionResult CreateStocks([FromBody] CreateStockDto newData)
    {
        if (newData == null)
        {
            return BadRequest();
        }
        var model = newData.ToStock();
        _context.Stock.Add(model);
        _context.SaveChanges();
        Console.WriteLine(nameof(GetStockById));
        return CreatedAtAction(nameof(GetStockById), new { model.Id }, model.ToStockDto());
    }

    [HttpPut("update/{id}")]
    [ProducesResponseType(statusCode: 204)]
    [ProducesResponseType(statusCode: 400)]
    public IActionResult UpdateStocks([FromRoute] int id, [FromBody] CreateStockDto updateData)
    {
        var dbStock = _context.Stock.Find(id);
        if (dbStock == null)
        {
            return BadRequest();
        }
        dbStock.Symbol = updateData.Symbol;
        dbStock.CompanyName = updateData.CompanyName;
        dbStock.MarketCap =  updateData.MarketCap;
        dbStock.Purchase= updateData.Purchase;
        dbStock.Industry = updateData.Industry;
        dbStock.Dividend = updateData.Dividend;
        _context.SaveChanges();
       return NoContent();
    }
}
