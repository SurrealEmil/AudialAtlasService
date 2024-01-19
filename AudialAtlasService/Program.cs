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
            builder.Services.AddScoped<IArtistHandler, ArtistHandler>();
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // Songs
            app.MapGet("/songs", SongHandler.ListAllSongs);
            app.MapGet("/songs/{songId}", SongHandler.GetSingleSong);
            app.MapPost("/songs", SongHandler.PostSong);

            // Artists
            app.MapGet("/artists", ArtistHandler.GetAllArtists);
            app.MapGet("/artists/{artistId}", ArtistHandler.GetSingleArtist);
            app.MapPost("/artists", ArtistHandler.PostArtist);

            // Genre
            app.MapGet("/genres", GenreHandler.ListAllGenre);
            app.MapGet("/genres/{genreId}", GenreHandler.GetSingleGenre);
            app.MapGet("/artists/genres/{genreId}", GenreHandler.GetAllArtistsInGenre);
            app.MapGet("/songs/genres/{genreId}", GenreHandler.GetAllSongsInGenre);
            app.MapPost("/genres", GenreHandler.PostGenre);

            // Link Genre to Artist
            app.MapPost("/artists/{artistId}/genres/{genreId}", ArtistHandler.LinkGenreToArtist);

            app.Run();
        }
    }
}
