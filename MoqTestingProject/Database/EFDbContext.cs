using Microsoft.EntityFrameworkCore;
using MoqTestingProject.Entities;

namespace MoqTestingProject.Database
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}

