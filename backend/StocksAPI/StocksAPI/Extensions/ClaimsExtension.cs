using StocksAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StocksAPI.Extensions;

public static class ClaimsExtension
{
    public static string? GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(x => x.Type.Equals(JwtRegisteredClaimNames.Name)).Value.ToString();
 
    }
}
