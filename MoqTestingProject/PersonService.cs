using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqTestingProject
{
    public class PersonService(IPersonRepository repository)
    {
        private readonly IPersonRepository _repository = repository;

        public async Task AddAsync(Person person)
        {
            await _repository.CreateAsync(person);
        }

        public async Task<bool> TryDeleteAsync(Person person)
        {
            return await _repository.TryDeleteAsync(person);
        }
        public Person? GetById(int id)
        {
            return _repository.GetById(id);
        }
        public IQueryable<Person> GetAll()
        {
            return _repository.GetAll();
        }

    }
}
