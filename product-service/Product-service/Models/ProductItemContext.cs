using Microsoft.EntityFrameworkCore;

namespace Product_service.Models;

public class ProductItemContext: DbContext
{
    public ProductItemContext(DbContextOptions<ProductItemContext> options)
        : base(options)
    {
    }

    public DbSet<Product> ProductItems { get; set; } = null!;
}