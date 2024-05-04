using System.ComponentModel.DataAnnotations;

namespace StocksAPI.Dto.StockDto;

public class CreateStockDto
{
    [Required]
    [MaxLength(10,ErrorMessage ="Symbol cannot be over 10 charcaters long")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(20, ErrorMessage = "Company Name cannot be over 20 charcaters long")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1,1000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0, 1000)]
    public decimal Dividend { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Industry cannot be over 20 charcaters long")]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(0, 5000000000)]
    public long MarketCap { get; set; }
}
