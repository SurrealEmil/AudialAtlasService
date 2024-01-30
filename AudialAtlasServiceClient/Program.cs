using AudialAtlasServiceClient.Screens;
using AudialAtlasServiceClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AudialAtlasServiceClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Audial Atlas Service";
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Build configuration to obtain and pass on the ApiBaseUrl from settings
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();


            // Set up dependency injection container with ApiService and HttpClient
            var serviceProvider = new ServiceCollection()
                        .AddHttpClient()
                        .AddScoped<IAudialAtlasApiService, AudialAtlasApiService>
                           (provider => new AudialAtlasApiService(
                               provider.GetRequiredService<IHttpClientFactory>(),
                               configuration))
                        .BuildServiceProvider();

            while (true)
            {
                Console.Clear();

                var apiService = serviceProvider.GetRequiredService<IAudialAtlasApiService>();

                var loginScreen = new LoginScreen(apiService);

                var audialAtlasApp = new AudialAtlasClientApplication(loginScreen);

                await audialAtlasApp.RunAsync();
            }
        }
    }
}

