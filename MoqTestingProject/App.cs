using Microsoft.Extensions.Logging;
using static System.Console;

namespace MoqTestingProject
{
    public class App(IPersonRepository repository, ILogger<App> logger)
    {
        private readonly IPersonRepository _repository = repository;
        private readonly ILogger<App> _logger = logger;

        public async Task RunAsync()
        {
            _logger.LogInformation("Starting application");
            var persons = _repository.GetAll();

            foreach (Person person in persons)
            {
                WriteLine(person.ToString());
            }

            _logger.LogInformation("Getting person with ID: 1");
            Person? johndoe = _repository.GetById(1);

            if (johndoe != null)
            {
                WriteLine("Person " + johndoe.ToString());
            }
        }
    }
}
