using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoqTestingProject
{
    public class PersonRepository(EFDbContext context) : IPersonRepository
    {
        private readonly EFDbContext _context = context;

        public async Task CreateAsync(Person person)
        {
            if (!_context.Persons.Any(x => x.PersonId == person.PersonId))
            {
                _context.Persons.Add(person);
            }
            await _context.SaveChangesAsync();
        }

        public Person? GetById(int id)
        {
            return _context.Persons.Where(x => id.Equals(x.PersonId)).AsNoTracking().FirstOrDefault();
        }

        public IQueryable<Person> GetAll()
        {
            return _context.Persons.AsNoTracking();
        }

        public async Task UpdateOrAddAsync(Person newPerson)
        {
            if (_context.Persons.Any(x => x.PersonId == newPerson.PersonId))
            {
                _context.Persons.Update(newPerson);
            } else
            {
                _context.Persons.Add(newPerson);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TryDeleteAsync(Person person)
        {
            if (_context.Persons.Any(x => x.PersonId == person.PersonId)) 
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
                return true;
            } 
            else return false;
        }
    }
}
