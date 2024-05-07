using Microsoft.AspNetCore.Identity;

namespace StocksAPI.Models
{
    public class WebUser:IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
