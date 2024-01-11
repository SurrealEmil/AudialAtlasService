using AudialAtlasService.Data;
using AudialAtlasService.Handlers;
using Microsoft.EntityFrameworkCore;

namespace AudialAtlasService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // Songs
            app.MapGet("/songs", SongHandler.ListAllSongs);
            app.MapGet("/songs/{songId}", SongHandler.GetSingleSong);
            app.MapPost("/songs", SongHandler.PostSong);

            app.Run();
        }
    }
}
