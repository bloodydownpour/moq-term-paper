using Microsoft.Extensions.Logging;
using static System.Console;

namespace MoqTestingProject
{
    public class App
    {
        private readonly PersonService _personService;
        private readonly ILogger<App> _logger;

        public App(PersonService service, ILogger<App> logger)
        {
            _personService = service;
            _logger = logger;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Starting application");

            var persons = _personService.GetAll();
            foreach (Person person in persons)
            {
                WriteLine(person.ToString());
            }

            _logger.LogInformation("Getting person with ID: 1");
            Person? johndoe = _personService.GetById(1);

            if (johndoe != null)
            {
                WriteLine("Person " + johndoe.ToString());
            }

            await Task.CompletedTask;
        }
    }
}
