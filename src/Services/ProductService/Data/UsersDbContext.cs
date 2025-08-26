using Microsoft.EntityFrameworkCore;

namespace ProductService.Data;

public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}

public record Product(int Id, string Sku, string Name, decimal Price);