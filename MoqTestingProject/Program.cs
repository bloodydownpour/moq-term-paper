using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace MoqTestingProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<EFDbContext>();
                    services.AddTransient<IPersonRepository, PersonRepository>();
                    services.AddScoped<PersonService>();
                    services.AddTransient<App>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var app = scope.ServiceProvider.GetRequiredService<App>();
            app.RunAsync();
        }
    }
}
