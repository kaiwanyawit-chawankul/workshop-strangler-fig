dotnet new webapi -n ProductService -o ./src/Services/ProductService

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```Data/UsersDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace ProductService.Data;

public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}

public record Product(int Id, string Sku, string Name, decimal Price);
```

```Program.cs
using Microsoft.EntityFrameworkCore;
using ProductService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductsDbContext>(o => o.UseSqlite("Data Source=products.db"));
var app = builder.Build();

app.MapGet("/products", async (ProductsDbContext db) => await db.Products.ToListAsync());
app.MapPost("/products", async (ProductsDbContext db, Product p) => { db.Products.Add(p); await db.SaveChangesAsync(); return Results.Created($"/products/{p.Id}", p); });

app.Run();

```

# from src/Services/ProductService
dotnet tool install --global dotnet-ef || true
dotnet ef migrations add Init
dotnet ef database update
DOTNET_URLS=http://localhost:5092 dotnet run


# monolith
DOTNET_URLS=http://localhost:5070 dotnet run
# user service
DOTNET_URLS=http://localhost:5091 dotnet run
# product service
DOTNET_URLS=http://localhost:5092 dotnet run
# gateway
DOTNET_URLS=http://localhost:5080 dotnet watch run
# AcceptanceTests
DOTNET_URLS=http://localhost:5080 dotnet test