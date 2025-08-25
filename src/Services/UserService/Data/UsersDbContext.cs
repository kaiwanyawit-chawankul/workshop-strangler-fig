using Microsoft.EntityFrameworkCore;

namespace UserService.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}

public record User(int Id, string Email, string Name);