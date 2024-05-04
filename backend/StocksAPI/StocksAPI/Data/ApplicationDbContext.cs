using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StocksAPI.Models;
using System;



namespace StocksAPI.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        List<IdentityRole> roles = new()
        {
            new IdentityRole() //standard syntax for creating objects
            {
                Name = "User",
                NormalizedName = "USER",
            },
            new IdentityRole //object initiator no paranthesis
            {
                Name = "admin",
                NormalizedName = "ADMIN",
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comment { get; set; }
}
