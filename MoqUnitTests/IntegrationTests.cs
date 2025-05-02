using Microsoft.Extensions.Logging;
using MoqTestingProject;
using MoqTestingProject.Database;
using MoqTestingProject.Entities;
using MoqTests.TestingSupplies;

namespace MoqTests
{
    public class IntegrationTests : IClassFixture<DatabaseCleaner>
    {
        public IntegrationTests() { }
        [Fact]
        public async Task CanInsertPerson()
        {
            var person = new Person(1, "Test", "User");
            var _cleaner = new DatabaseCleaner();
            _cleaner.Context.Persons.Add(person);
            _cleaner.Context.SaveChanges();

            var insertedUser = _cleaner.Context.Persons.First();
            await _cleaner.CleanDatabase();

            Assert.Equal("1: Test User", insertedUser.ToString());
        }

        [Fact]
        public async Task CanUpdatePerson()
        {
            var person = new Person(2, "Test", "User");
            var _cleaner = new DatabaseCleaner();

            _cleaner.Context.Persons.Add(person);
            _cleaner.Context.SaveChanges();

            person.Surname = "Updated User";

            _cleaner.Context.SaveChanges();

            var updatedUser = _cleaner.Context.Persons.First();
            await _cleaner.CleanDatabase();

            Assert.Equal("2: Test Updated User", updatedUser.ToString());
        }
        [Fact]
        public async Task CanDeletePerson()
        {
            var _cleaner = new DatabaseCleaner();
            await _cleaner.CleanDatabase();
            var repoLogger = new LoggerFactory().CreateLogger<App>();
            IPersonRepository repos = new PersonRepository(_cleaner.Context, repoLogger);

            await repos.UpdateOrAddAsync(new Person(1, "John", "Doe"));
            await repos.UpdateOrAddAsync(new Person(2, "Jane", "Smith"));
            await repos.UpdateOrAddAsync(new Person(3, "Алёна", "Найдёнова"));
            await _cleaner.Context.SaveChangesAsync();

            var deletedPerson = repos.GetById(3);
            Assert.True(await repos.TryDeleteAsync(deletedPerson));
            await _cleaner.Context.SaveChangesAsync();

            Assert.Equal(2, repos.GetAll().Count());
        }

        [Fact]
        public async Task CantDeleteNonExistentPerson()
        {
            var _cleaner = new DatabaseCleaner();
            await _cleaner.CleanDatabase();
            var repoLogger = new LoggerFactory().CreateLogger<App>();
            IPersonRepository repos = new PersonRepository(_cleaner.Context, repoLogger);

            await repos.UpdateOrAddAsync(new Person(1, "John", "Doe"));
            await repos.UpdateOrAddAsync(new Person(2, "Jane", "Smith"));
            await repos.UpdateOrAddAsync(new Person(3, "Алёна", "Найдёнова"));
            await _cleaner.Context.SaveChangesAsync();

            Assert.False(await repos.TryDeleteAsync(new Person(4, "Несуществующий", "Человек")));
            await _cleaner.Context.SaveChangesAsync();

            Assert.Equal(3, repos.GetAll().Count());
        }

        [Fact]
        public async Task CanGetPerson()
        {
            var _cleaner = new DatabaseCleaner();
            await _cleaner.CleanDatabase();
            var repoLogger = new LoggerFactory().CreateLogger<App>();
            IPersonRepository repos = new PersonRepository(_cleaner.Context, repoLogger);
            await repos.UpdateOrAddAsync(new Person(3, "Алёна", "Найдёнова"));
            await _cleaner.Context.SaveChangesAsync();

            var person = repos.GetById(3);
            Assert.Equal("3: Алёна Найдёнова", person.ToString());
        }
        [Fact]
        public async Task CantGetNonExistentPerson()
        {
            var _cleaner = new DatabaseCleaner();
            await _cleaner.CleanDatabase();
            var repoLogger = new LoggerFactory().CreateLogger<App>();
            IPersonRepository repos = new PersonRepository(_cleaner.Context, repoLogger);

            await repos.UpdateOrAddAsync(new Person(1, "John", "Doe"));
            await repos.UpdateOrAddAsync(new Person(2, "Jane", "Smith"));
            await repos.UpdateOrAddAsync(new Person(3, "Алёна", "Найдёнова"));
            await _cleaner.Context.SaveChangesAsync();

            Assert.Null(repos.GetById(4));
        }
    }
}
