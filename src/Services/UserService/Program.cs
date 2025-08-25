using Microsoft.EntityFrameworkCore;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UsersDbContext>(o => o.UseSqlite("Data Source=users.db"));
var app = builder.Build();

app.MapGet("/users", async (UsersDbContext db) => await db.Users.ToListAsync());
app.MapPost("/users", async (UsersDbContext db, User u) => { db.Users.Add(u); await db.SaveChangesAsync(); return Results.Created($"/users/{u.Id}", u); });

app.Run();
