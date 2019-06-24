using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrefactorApi.Context
{
  public class UserDbContext : DbContext
  {
    public UserDbContext(DbContextOptions options) : base(options)
    {

    }

    DbSet<User> Users { get; set; }
  }
}
