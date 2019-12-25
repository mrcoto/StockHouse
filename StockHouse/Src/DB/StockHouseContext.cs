using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockHouse.Src.Models;

namespace StockHouse.Src.DB
{
    /// <summary>
    /// Database context
    /// </summary>
    public partial class StockHouseContext : DbContext
    {
        /// <summary>
        /// Net Core 3.0 logger factory
        /// </summary>
        /// <value>Logger factory</value>
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter("Microsoft", LogLevel.Information)
                   .AddFilter("System", LogLevel.Warning)
                   .AddFilter("StockHouse.Program", LogLevel.Information)
                   .AddConsole();
        });

        /// <summary>
        /// Entity Representation of Product.
        /// </summary>
        /// <value>Entity Representation of Product</value>
        public virtual DbSet<Product> Products { get; set; }
        /// <summary>
        /// Entity Representation of ProductHasProducts.
        /// </summary>
        /// <value>Entity Representation of ProductHasProducts</value>
        public virtual DbSet<ProductHasProduct> ProductHasProducts { get; set; }
        /// <summary>
        /// Entity Representation of Warehouse.
        /// </summary>
        /// <value>Entity Representation of Warehouse</value>
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        /// <summary>
        /// Entity Representation of WarehouseHasProducts.
        /// </summary>
        /// <value>Entity Representation of Product</value>
        public virtual DbSet<WarehouseHasProduct> WarehouseHasProducts { get; set; }

        /// <summary>
        /// Definition of keys and relationships between entities
        /// </summary>
        /// <param name="modelBuilder">Model Builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProductHasProduct>(php =>
                {
                    php.HasKey(php => new {php.ProductId, php.ProductContentId});
                    php.HasOne(php => php.Product)
                       .WithMany(p => p.ProductComposition)
                       .HasForeignKey(php => php.ProductId);
                    php.HasOne(php => php.ProductContent)
                       .WithMany(p => p.ContainedIn)
                       .HasForeignKey(php => php.ProductContentId);
                });
            modelBuilder
                .Entity<WarehouseHasProduct>(whp => 
                {
                    whp.HasKey(whp => new {whp.WarehouseId, whp.ProductId});
                });
        }

        /// <summary>
        /// Database and logging configuration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseLoggerFactory(loggerFactory)
                          .UseNpgsql("Host=localhost;Database=stock_house;Username=postgres;Password=secret");
        }

        
    }
}