using StocksAPI.Models;

namespace StocksAPI.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(WebUser user);
    }
}
