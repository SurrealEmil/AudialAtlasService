using AudialAtlasService.Data;
using AudialAtlasService.Handlers;
using Microsoft.EntityFrameworkCore;
using AudialAtlasService.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Repositories;
using AudialAtlasService.Services.DeezerService;
using AudialAtlasService.Services.DeezerService.Models.ViewModels;
using AudialAtlasService.Services.DeezerService.Handler;

namespace AudialAtlasService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
            
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            // DI Containers med AddScoped
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<ISongRepository, SongRepository>();

            builder.Services.AddScoped<IGenreRepository, GenreRepository>();

            // Testing Deezer Service
            builder.Services.AddScoped<IDeezerSearchFunction, DeezerSearchService>();

            var app = builder.Build();

            app.MapGet("/", () => 
            {
                Console.WriteLine("Hello World in Console!");
            });

            // Users
            app.MapPost("/users/connect-to-artist", UserHandler.ConnectUserToArtistHandler);
            app.MapPost("/users/connect-to-genre", UserHandler.ConnectUserToGenreHandler);
            app.MapPost("/users/connect-to-song", UserHandler.ConnectUserToSongHandler);
            app.MapGet("/users/allusers", UserHandler.GetAllUsersFromRepository); // Ny
            app.MapGet("/users/{userId}/artists", UserHandler.GetAllArtistsLikedByUserHandler);
            app.MapGet("/users/{userId}/genres", UserHandler.GetAllGenresLikedByUserHandler);
            app.MapGet("/users/{userId}/songs", UserHandler.GetAllSongsLikedByUserHandler);
            app.MapGet("/users/{userName}/check", UserHandler.CheckIfUserExistsHandler);
            app.MapGet("/users/login/{userName}/{password}", UserHandler.UserAuthentication);
            app.MapPost("/users", UserHandler.AddUser);


            // Songs
            app.MapGet("/songs", SongHandler.ListAllSongs);
            app.MapGet("/songs/{songId}", SongHandler.GetSingleSong);
            // PostSong posts the song directly on the artist. So currently no way of posting a song
            // without also having an artist.
            app.MapPost("artists/{artistId}/songs", SongHandler.PostSong);

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
            app.MapPost("/songs/{songId}/genres/{genreId}", SongHandler.LinkGenreToSong);

            // Testing Deezer Service
            //app.MapGet("/deezer/{artistNameQuery}", async (IDeezerSearchFunction service, string artistNameQuery) =>
            //{
            //    DeezerArtistViewModel result = await service.GetArtistViaSearchStringAsync(artistNameQuery);

            //    return Results.Json(result);
            //});
            app.MapGet("/deezer/{artistNameQuery}/topfivesongs", DeezerServiceHandler.GetTopFiveSongsOfArtist);

            app.Run();
        }
    }
}
