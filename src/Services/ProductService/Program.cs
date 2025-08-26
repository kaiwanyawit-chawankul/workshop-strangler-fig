using Microsoft.EntityFrameworkCore;
using ProductService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductsDbContext>(o => o.UseSqlite("Data Source=products.db"));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
    await db.Database.MigrateAsync();
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product(1, "SKU-1", "Keyboard", 49.9m),
            new Product(2, "SKU-2", "Mouse", 29.9m)
        );
        await db.SaveChangesAsync();
    }
}
app.MapGet("/products", async (ProductsDbContext db) => await db.Products.ToListAsync());
app.MapPost("/products", async (ProductsDbContext db, Product p) => { db.Products.Add(p); await db.SaveChangesAsync(); return Results.Created($"/products/{p.Id}", p); });

app.Run();
