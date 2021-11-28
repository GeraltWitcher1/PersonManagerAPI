using Microsoft.EntityFrameworkCore;
using PersonManagerAPI.Models;

namespace PersonManagerAPI.Persistence
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Adult> Adults { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Persons.db");
        }
    }
}