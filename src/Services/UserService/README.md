dotnet new webapi -n UserService -o ./src/Services/UserService

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```Data/UsersDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace UserService.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}

public record User(int Id, string Email, string Name);
```

```Program.cs
using Microsoft.EntityFrameworkCore;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UsersDbContext>(o => o.UseSqlite("Data Source=users.db"));
var app = builder.Build();

app.MapGet("/users", async (UsersDbContext db) => await db.Users.ToListAsync());
app.MapPost("/users", async (UsersDbContext db, User u) => { db.Users.Add(u); await db.SaveChangesAsync(); return Results.Created($"/users/{u.Id}", u); });

app.Run();
```

# from src/Services/UserService
dotnet tool install --global dotnet-ef || true
dotnet ef migrations add Init
dotnet ef database update
DOTNET_URLS=http://localhost:5091 dotnet run


# monolith
DOTNET_URLS=http://localhost:5070 dotnet run
# user service
DOTNET_URLS=http://localhost:5091 dotnet run
# gateway
DOTNET_URLS=http://localhost:5080 dotnet watch run
# AcceptanceTests
dotnet test