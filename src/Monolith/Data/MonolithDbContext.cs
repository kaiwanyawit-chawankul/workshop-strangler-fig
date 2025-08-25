using Microsoft.EntityFrameworkCore;

namespace Monolith.Data;

public class MonolithDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();

    public MonolithDbContext(DbContextOptions<MonolithDbContext> options)
        : base(options) { }
}

public record User(int Id, string Email, string Name);

public record Product(int Id, string Sku, string Name, decimal Price);

public record Order(int Id, int UserId, int ProductId, int Quantity, DateTime CreatedAt);
