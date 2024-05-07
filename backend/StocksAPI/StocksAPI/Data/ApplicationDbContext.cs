using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StocksAPI.Models;



namespace StocksAPI.Data;

public class ApplicationDbContext : IdentityDbContext<WebUser>
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Portfolio>(x => x.HasKey(p => new { p.UserId, p.StockId }));

        builder.Entity<Portfolio>()
            .HasOne(u => u.User)
            .WithMany(p => p.Portfolios)
            .HasForeignKey(p => p.UserId);
        builder.Entity<Portfolio>()
            .HasOne(s => s.Stock)
            .WithMany(p=>p.Portfolios)
            .HasForeignKey(p => p.StockId);

        List<IdentityRole> roles = new()
        {
            new IdentityRole() //standard syntax for creating objects
            {
                Name = "user",
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
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
}
