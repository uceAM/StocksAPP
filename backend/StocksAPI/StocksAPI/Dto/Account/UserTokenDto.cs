﻿using System.ComponentModel.DataAnnotations;

namespace StocksAPI.Dto.Account;

public class UserTokenDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Token { get; set; }
}
