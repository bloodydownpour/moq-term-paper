using Moq;
using MoqTestingProject;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace MoqTests;

public class UnitTests
{
    public UnitTests() { }
    [Fact]
    public void GetPerson_Returns_Person()
    {
        var mockRepo = new Mock<IPersonRepository>();
        mockRepo.Setup(r => r.GetById(1)).Returns(new Person(1, "John", "Doe"));

        var service = new PersonService(mockRepo.Object);

        var result = service.GetById(1);
        Assert.Equal(new Person(1, "John", "Doe").ToString(), result.ToString());
    }


    [Fact]
    public void GetAllPersons_Returns_IQueryable_Of_Persons()
    {
        var mockRepo = new Mock<IPersonRepository>();
        var persons = new List<Person>
        {
            new(1, "John", "Doe"),
            new(2, "Jane", "Smith"),
            new(3, "Алёна", "Найдёнова")
        }.AsQueryable();
        mockRepo.Setup(repo => repo.GetAll()).Returns(persons);

        var service = new PersonService(mockRepo.Object);

        var result = service.GetAll();

        Assert.Equal(3, result.Count());
        Assert.Contains(result, p => p.Name == "John");
        Assert.Contains(result, p => p.Surname == "Smith");
    }
    [Fact]
    public async Task AddPerson_Should_Call_Repository_UpdateOrAddAsync()
    {
        var mockRepo = new Mock<IPersonRepository>();
        var person = new Person(1, "John", "Doe");
        var service = new PersonService(mockRepo.Object);
        await service.UpdateOrAddAsync(person);
        mockRepo.Verify(r => r.UpdateOrAddAsync(It.Is<Person>(u => u == person)), Times.Once);
    }
    [Fact]
    public async Task TryDeletePerson_Should_Call_Repository_TryDeleteAsync()
    {
        var mockRepo = new Mock<IPersonRepository>();
        var person = new Person(1, "John", "Doe");
        var service = new PersonService(mockRepo.Object);
        await service.TryDeleteAsync(person);
        mockRepo.Verify(r => r.TryDeleteAsync(It.Is<Person>(u => u == person)), Times.Once);
    }
}
