using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
    : base(options)
    {
    }

    public DbSet<Product> Products {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
        });

        //Seed intital data
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High-performance laptop for development",
                Price = 1299.99m,
                StockQuantity = 50,
                Category = "Electronics"
            },
            new Product
            {
                Id = 2,
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse",
                Price = 29.99m,
                StockQuantity = 200,
                Category = "Accessories"
            },
            new Product
            {
                Id = 3,
                Name = "Mechanical Keyboard",
                Description = "RGB mechanical keyboard",
                Price = 149.99m,
                StockQuantity = 75,
                Category = "Accessories"
            }
        );
    }
}