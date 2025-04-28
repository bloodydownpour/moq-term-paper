using MoqTestingProject;
using MoqTests.TestingSupplies;

namespace MoqTests
{
    public class IntegrationTests : IClassFixture<DatabaseCleaner>
    {
        private readonly DatabaseCleaner _cleaner;

        public IntegrationTests(DatabaseCleaner cleaner) => _cleaner = cleaner;

        [Fact]
        public async Task CanInsertPerson()
        {
            var person = new Person(1, "Test", "User");

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

            _cleaner.Context.Persons.Add(person);
            _cleaner.Context.SaveChanges();

            person.Surname = "Updated User";

            _cleaner.Context.SaveChanges();

            var updatedUser = _cleaner.Context.Persons.First(); // заново берём из БД
            await _cleaner.CleanDatabase();

            Assert.Equal("2: Test Updated User", updatedUser.ToString());
        }


    }
}
