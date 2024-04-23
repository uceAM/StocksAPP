using StocksAPI.Dto.Stock;
using StocksAPI.Dto.StockDto;
using StocksAPI.Models;
using System;

namespace StocksAPI.Mapper;

public static class StockMapper
{
    public static StockDto ToStockDto(this Stock StockModel)
    {
        return new StockDto
        {
            Id = StockModel.Id,
            Symbol = StockModel.Symbol,
            CompanyName = StockModel.CompanyName,
            Purchase = StockModel.Purchase,
            Dividend = StockModel.Dividend,
            Industry = StockModel.Industry,
            MarketCap = StockModel.MarketCap,
        };
    }
    public static Stock ToStock(this CreateStockDto newData)
    {
        return new Stock
        {
            Symbol = newData.Symbol,
            CompanyName = newData.CompanyName,
            Purchase = newData.Purchase,
            Dividend = newData.Dividend,
            Industry = newData.Industry,
            MarketCap = newData.MarketCap,
        };
    }
}
