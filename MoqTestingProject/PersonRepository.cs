using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MoqTestingProject
{
    public class PersonRepository(EFDbContext context, ILogger<App> logger) : IPersonRepository
    {
        private readonly EFDbContext _context = context;
        private readonly ILogger<App> _logger = logger;

        public Person? GetById(int id)
        {
            _logger.LogInformation($"Trying to get person with ID: {id}");
            return _context.Persons.Where(x => id.Equals(x.PersonId)).AsNoTracking().FirstOrDefault();
        }

        public IQueryable<Person> GetAll()
        {
            _logger.LogInformation($"Getting all persons from database");
            return _context.Persons.AsNoTracking().OrderBy(x => x.PersonId);
        }

        public async Task UpdateOrAddAsync(Person newPerson)
        {
            var existingPerson = await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.PersonId == newPerson.PersonId);
            _logger.LogInformation($"Trying to update/add person with ID: {newPerson.PersonId}");
            if (existingPerson != null)
            {
                _logger.LogInformation($"ID: {newPerson.PersonId} has been updated");
                _context.Persons.Update(newPerson);
            } 
            else
            {
                _logger.LogInformation($"ID: {newPerson.PersonId} has been added");
                _context.Persons.Add(newPerson);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TryDeleteAsync(Person person)
        {
            _logger.LogInformation($"Trying to delete person with ID {person.PersonId}");
            var personToDelete = _context.Persons.FirstOrDefault(x => x.PersonId == person.PersonId);
            if (personToDelete != null)
            {
                _logger.LogInformation($"Person with ID {person.PersonId} has been successfully deleted");
                _context.Persons.Remove(personToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                _logger.LogError($"Person with ID {person.PersonId} not found, therefore, it cannot be deleted");
                return false;
            }
        }
    }
}
