using AudialAtlasService.Data;
using AudialAtlasServiceClient.Services;
using AudialAtlasServiceClient.Screens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AudialAtlasServiceClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Audial Atlas Service";
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            // Get API base URL from configuration
            string apiBaseUrl = configuration["ApiSettings:BaseUrl"];

            // Configure DbContextOptions with connection string from appsettings
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApplicationContext"));

            while (true)
            {
                using var context = new ApplicationContext(optionsBuilder.Options);

                Console.Clear();
                await LoginScreen.LoginMenuAsync(context, apiBaseUrl);
            }
        }
    }
}

