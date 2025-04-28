using Microsoft.EntityFrameworkCore;
using MoqTestingProject.Database;

namespace MoqTests.TestingSupplies
{
    public class DatabaseCleaner : IDisposable
    {
        public EFDbContext Context { get; private set; }

        public DatabaseCleaner()
        {
            var options = new DbContextOptionsBuilder<EFDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           .Options;

            Context = new EFDbContext(options);
        }

        public async Task CleanDatabase()
        {
            var persons = Context.Persons.ToList();
            Context.Persons.RemoveRange(persons);
            await Context.SaveChangesAsync();
        }


        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }
    }
}
