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

           
            while (true)
            {
                WriteLine("\n\nChoose your action:\n1) Get all persons;\n2) Get person by ID;\n" +
               "3) Add person (ID will assign automatically);\n4) Update/create person(You will assign ID);\n" +
               "5) Delete person;\n6) Exit;");
                string? choice = ReadLine();
                if (choice != null)
                {
                    switch (choice)
                    {
                        case "1":
                            {
                                var persons = _personService.GetAll();
                                foreach (var person in persons)
                                {
                                    WriteLine(person.ToString());
                                }
                                break;
                            }
                        case "2":
                            {
                                Write("Enter person's ID: ");
                                int id = Convert.ToInt32(ReadLine());
                                Person? person = _personService.GetById(id);
                                if (person != null)
                                {
                                    WriteLine(person.ToString());
                                }
                                break;
                            }
                        case "3":
                            {
                                WriteLine("Enter person's name: ");
                                string? name = ReadLine();
                                WriteLine("Enter person's surname: ");
                                string? surname = ReadLine();
                                var persons = _personService.GetAll();
                                if (name != null && surname != null)
                                {
                                    await _personService.UpdateOrAddAsync(new Person(persons.Count() + 1, name, surname));
                                }
                                break;
                            }
                        case "4":
                            {
                                Write("Enter person's ID: ");
                                int id = Convert.ToInt32(ReadLine());
                                WriteLine("Enter person's name: ");
                                string? name = ReadLine();
                                WriteLine("Enter person's surname: ");
                                string? surname = ReadLine();
                                if (name != null && surname != null)
                                {
                                    await _personService.UpdateOrAddAsync(new Person(id, name, surname));
                                }
                                break;
                            }
                        case "5":
                            {
                                Write("Enter person's ID: ");
                                int id = Convert.ToInt32(ReadLine());
                                await _personService.TryDeleteAsync(_personService.GetById(id));
                                break;
                            }
                        case "6":
                            {
                                return;
                            }
                        default:
                            {
                                throw new Exception("Choice was not recognized");
                            }
                    }
                }


            }
        }
    }
}
