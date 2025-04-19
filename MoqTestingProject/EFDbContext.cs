using Microsoft.EntityFrameworkCore;

namespace MoqTestingProject
{
    public class EFDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MoqTesting;Username=postgres;Password=root");
        }
    }
}

