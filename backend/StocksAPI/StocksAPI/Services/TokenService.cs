﻿using Microsoft.IdentityModel.Tokens;
using StocksAPI.Interfaces;
using StocksAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StocksAPI.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s:_config["JWT:SigningKey"]));
    }
    public string CreateToken(WebUser user)
    {
        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
        };
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(30),
            SigningCredentials = credentials,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"],
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenObj = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(tokenObj);
    }
}
