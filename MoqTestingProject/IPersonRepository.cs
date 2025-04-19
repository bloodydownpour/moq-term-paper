namespace MoqTestingProject
{
    public interface IPersonRepository
    {
        Task CreateAsync(Person person);
        Task<bool> TryDeleteAsync(Person person);
        Person? GetById(int id);
        Task UpdateOrAddAsync(Person newPerson);
        IQueryable<Person> GetAll();
    }
}
