using Microsoft.EntityFrameworkCore;
using Monolith.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MonolithDbContext>(opt => opt.UseSqlite("Data Source=monolith.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MonolithDbContext>();
    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User(1, "ada@example.com", "Ada"),
            new User(2, "linus@example.com", "Linus"));
        db.Products.AddRange(
            new Product(1, "SKU-1", "Keyboard", 49.9m),
            new Product(2, "SKU-2", "Mouse", 29.9m));
        db.Orders.Add(new Order(1, 1, 1, 1, DateTime.UtcNow));
        db.SaveChanges();
    }
}
app.MapDelete("/clear", async (MonolithDbContext db) =>
{
    await db.Orders.ExecuteDeleteAsync();
    await db.Products.ExecuteDeleteAsync();
    await db.Users.ExecuteDeleteAsync();

    await db.SaveChangesAsync();
});

app.MapGet("/users", async (MonolithDbContext db) => await db.Users.ToListAsync());
app.MapPost(
    "/users",
    async (MonolithDbContext db, User u) =>
    {
        db.Users.Add(u);
        await db.SaveChangesAsync();
        return Results.Created($"/users/{u.Id}", u);
    }
);

app.MapGet("/products", async (MonolithDbContext db) => await db.Products.ToListAsync());
app.MapPost(
    "/products",
    async (MonolithDbContext db, Product p) =>
    {
        db.Products.Add(p);
        await db.SaveChangesAsync();
        return Results.Created($"/products/{p.Id}", p);
    }
);

app.MapGet("/orders", async (MonolithDbContext db) => await db.Orders.ToListAsync());
app.MapPost(
    "/orders",
    async (MonolithDbContext db, Order o) =>
    {
        db.Orders.Add(o);
        await db.SaveChangesAsync();
        return Results.Created($"/orders/{o.Id}", o);
    }
);

app.Run();
