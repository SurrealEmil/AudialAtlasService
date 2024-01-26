using AudialAtlasService.Data;
using AudialAtlasService.Handlers;
using Microsoft.EntityFrameworkCore;
using AudialAtlasService.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Repositories;

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

            var app = builder.Build();

            app.MapGet("/", () => 
            {
                Console.WriteLine("Hello World in Console!");
            });

            // Users
            app.MapPost("/users/connect-to-artist", UserHandler.ConnectUserToArtistHandler);
            app.MapPost("/users/connect-to-genre", UserHandler.ConnectUserToGenreHandler);
            app.MapPost("/users/connect-to-song", UserHandler.ConnectUserToSongHandler);
            app.MapGet("/users/{userId}/artists", UserHandler.GetAllArtistsLikedByUserHandler);
            app.MapGet("/users/{userId}/genres", UserHandler.GetAllGenresLikedByUserHandler);
            app.MapGet("/users/{userId}/songs", UserHandler.GetAllSongsLikedByUserHandler);
            app.MapGet("/users/{userName}/check", UserHandler.CheckIfUserExistsHandler);

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

            // Link Artist to Genre
            app.MapPost("/artists/{artistId}/genres/{genreId}", ArtistHandler.LinkGenreToArtist);

            app.Run();
        }
    }
}
