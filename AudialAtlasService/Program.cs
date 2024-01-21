using AudialAtlasService.Data;
using AudialAtlasService.Handlers;
using Microsoft.EntityFrameworkCore;
using AudialAtlasService.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using AudialAtlasService.Models.DTOs;

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

            var app = builder.Build();

            app.MapGet("/", () => 
            {
                Console.WriteLine("Hello World");
            });

            // Users -----------------------------------------------------------
            app.MapGet("/users/{userId}/genres", (IGetUserFunctions userFunctions, int userId) =>
            {   
                var getGenresConnectedToUser = userFunctions.GetAllGenresLikedByUser(userId);

                // En f�rkortning p� en if-else sats.
                return getGenresConnectedToUser.Any() 
                ? Results.Ok(getGenresConnectedToUser) 
                : Results.NotFound("No genres found for this user.") ;
            });

            // G�r om
            app.MapGet("/users/{userName}/check", (IPostUserFunctions userFunctions, string userName) =>
            {
                try
                {
                    Console.WriteLine(userName);
                    bool userExistsOrNot = userFunctions.CheckIfUserExists(userName);

                    // En f�rkortning p� en if-else sats.
                    return userExistsOrNot
                    ? Results.Ok("User exists.")
                    : Results.NotFound("User does not exist.");
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            app.MapPost("/users/{userName}/artists/{artistId}", (string userName, int artistId, IPostUserFunctions userFunctions) =>
            {// Kanske b�r vara UserHandler userHanlder ist�llet f�r IPostUserFunctions, inte testat �n.
                userFunctions.ConnectUserToArtist(userName, artistId);
                return Results.Ok("Successfully connected user to artist.");
            });

            app.MapPost("/users/{userId}/genres/{genreId}", (int userId, int genreId, IPostUserFunctions userFunctions) =>
            {// Kanske b�r vara UserHandler userHanlder ist�llet f�r IPostUserFunctions, inte testat �n.
                userFunctions.ConnectUserToGenre(userId, genreId);
                return Results.Ok("Successfully connected user to genre.");
            });

            app.MapPost("/users/removeuser/{userName}", (IPostUserFunctions userFunctions, string userName) =>
            {

                try
                {
                    userFunctions.RemoveUser(userName);
                    return Results.Ok("Removed user sucessfully.");
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });
            //------------------------------------------------------------------

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
