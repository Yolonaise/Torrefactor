using Microsoft.EntityFrameworkCore;

namespace TorrefactorApi.Context
{
  public class UserDbContext : DbContext
  {
    public UserDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Application> Applications { get; set; }
  }
}
