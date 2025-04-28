using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace MoqTestingProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<EFDbContext>(options =>
                    {
                        options.UseNpgsql("Host=localhost;Port=5432;Database=MoqTesting;Username=postgres;Password=root");

                    });
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
            await app.RunAsync();
        }
    }
}
