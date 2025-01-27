﻿using System.ComponentModel.DataAnnotations;

namespace StocksAPI.Dto.Account
{
    public class NewUserDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
