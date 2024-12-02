using Microsoft.EntityFrameworkCore;

namespace MyWebApi1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { } 

        public DbSet<users> users { get; set; }


    }
}
