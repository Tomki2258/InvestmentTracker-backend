using InvestmentTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentTracker_backend;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }
    public DbSet<Stock> stocks { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<StockPosition>  stockPositions { get; set; }
    public DbSet<Dividend> dividends { get; set; }
    public DbSet<StockPriceHistory> stockPriceHistories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StockPosition>()
            .HasOne(sp => sp.User)
            .WithMany()
            .HasForeignKey(sp => sp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StockPosition>()
            .HasOne(sp => sp.Stock)
            .WithMany()
            .HasForeignKey(sp => sp.StockId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}