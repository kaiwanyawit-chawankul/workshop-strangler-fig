using Microsoft.EntityFrameworkCore;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UsersDbContext>(o => o.UseSqlite("Data Source=users.db"));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
    await db.Database.MigrateAsync();
    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User(1, "ada@example.com", "Ada"),
            new User(2, "linus@example.com", "Linus")
        );
        await db.SaveChangesAsync();
    }
}

app.MapGet("/users", async (UsersDbContext db) => await db.Users.ToListAsync());
app.MapPost(
    "/users",
    async (UsersDbContext db, User u) =>
    {
        db.Users.Add(u);
        await db.SaveChangesAsync();
        return Results.Created($"/users/{u.Id}", u);
    }
);

app.Run();
