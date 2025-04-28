using Moq;
using MoqTestingProject;

namespace MoqTests;

public class UnitTests
{
    public UnitTests() { }
    [Fact]
    public void GetPerson_ReturnsJohnDoe()
    {
        var mockRepo = new Mock<IPersonRepository>();
        mockRepo.Setup(r => r.GetById(1)).Returns(new Person(1, "John", "Doe"));

        var service = new PersonService(mockRepo.Object);

        var result = service.GetById(1);
        Assert.Equal(new Person(1, "John", "Doe").ToString(), result.ToString());
    }


    [Fact]
    public void GetAllPersons_ReturnsIQueryableOfPersons()
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
}
