using Microsoft.EntityFrameworkCore;
using osd_backend.Models;

namespace IDO.DAL
{
    public class MyAppDbContext: DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {         
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
    }
}
