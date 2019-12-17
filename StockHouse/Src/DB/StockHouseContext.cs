using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using StockHouse.Src.Models;

namespace StockHouse.Src.DB
{
    public partial class StockHouseContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter("Microsoft", LogLevel.Information)
                   .AddFilter("System", LogLevel.Warning)
                   .AddFilter("StockHouse.Program", LogLevel.Information)
                   .AddConsole();
        });

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseLoggerFactory(loggerFactory)
                          .UseNpgsql("Host=localhost;Database=stock_house;Username=postgres;Password=secret");
        }

        
    }
}