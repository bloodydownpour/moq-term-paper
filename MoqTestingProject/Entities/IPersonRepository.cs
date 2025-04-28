namespace MoqTestingProject.Entities
{
    public interface IPersonRepository
    {
        Task<bool> TryDeleteAsync(Person person);
        Person? GetById(int id);
        Task UpdateOrAddAsync(Person newPerson);
        IQueryable<Person> GetAll();
    }
}
