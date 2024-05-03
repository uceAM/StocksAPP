using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace StocksAPI.Helpers
{
    public class StockQueryObject
    {
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 charcaters long")]
        public string? Symbol { get; set; } = null;
        [MaxLength(20, ErrorMessage = "Company Name cannot be over 20 charcaters long")]
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool SortOrder { get; set; } = false;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 20;
    }
}
