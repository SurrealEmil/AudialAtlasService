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
            builder.Services.AddScoped<IGetUserFunctions, UserHandler>();
            builder.Services.AddScoped<IPostUserFunctions, UserHandler>();
            builder.Services.AddScoped<IViewModelFunctions, UserHandler>();

            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<ISongRepository, SongRepository>();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // Users -----------------------------------------------------------
            app.MapGet("/users/{userId}/genres", (IGetUserFunctions userFunctions, int userId) =>
            {   
                var getGenresConnectedToUser = userFunctions.GetAllGenresLikedByUser(userId);

                // En förkortning på en if-else sats.
                return getGenresConnectedToUser.Any() 
                ? Results.Ok(getGenresConnectedToUser) 
                : Results.NotFound("No genres found for this user.") ;
            });

            // Gör om
            app.MapPost("/users/{userId}/checkuserexists", (IPostUserFunctions userFunctions, UserDTO userDTO) =>
            {
                bool userExistsOrNot = userFunctions.CheckIfUserExists(userDTO);

                // En förkortning på en if-else sats.
                return userExistsOrNot 
                ? Results.Ok("User exists.") 
                : Results.NotFound("User does not exist.");
            });

            app.MapPost("/users/{userName}/artists/{artistId}", (string userName, int artistId, IPostUserFunctions userFunctions) =>
            {// Kanske bör vara UserHandler userHanlder istället för IPostUserFunctions, inte testat än.
                userFunctions.ConnectUserToArtist(userName, artistId);
                return Results.Ok("Successfully connected user to artist.");
            });

            app.MapPost("/users/{userId}/genres/{genreId}", (int userId, int genreId, IPostUserFunctions userFunctions) =>
            {// Kanske bör vara UserHandler userHanlder istället för IPostUserFunctions, inte testat än.
                userFunctions.ConnectUserToGenre(userId, genreId);
                return Results.Ok("Successfully connected user to genre.");
            });
            //------------------------------------------------------------------

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

            app.Run();
        }
    }
}
