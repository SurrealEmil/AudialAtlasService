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

            // Users -----------------------------------------------------------

            app.MapGet("/users/{userId}/artists", (IUserRepository userfunctions, int userId) =>
            {
                



                //Console.WriteLine("\nCONSOLE LOG\nINFO: GetAllArtistsLikedByUser HAS BEEN CALLED\n");

                //var getArtistConnectedToUser = userfunctions.GetAllArtistsLikedByUser(userId);

                //return getArtistConnectedToUser.Any()
                //? Results.Ok(getArtistConnectedToUser)
                //: Results.NotFound("No artists liked by this user.");
            });

            app.MapGet("/users/{userId}/genres", (IUserRepository userFunctions, int userId) =>
            {
                Console.WriteLine("\nCONSOLE LOG\nINFO: GetAllGenresLikedByUser HAS BEEN CALLED\n");
                var getGenresConnectedToUser = userFunctions.GetAllGenresLikedByUser(userId);

                // En förkortning på en if-else sats.
                return getGenresConnectedToUser.Any() 
                ? Results.Ok(getGenresConnectedToUser) 
                : Results.NotFound("No genres found for this user.");
            });

            app.MapGet("/users/{userName}/check", (IUserRepository userFunctions, string userName) =>
            {
                try
                {
                    Console.WriteLine("INFO: CheckIfUserExists HAS BEEN CALLED");
                    bool userExistsOrNot = userFunctions.CheckIfUserExists(userName);

                    // En förkortning på en if-else sats.
                    return userExistsOrNot
                    ? Results.Ok("User exists.")
                    : Results.NotFound("User does not exist.");
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            app.MapPost("/users/{userName}/artists/{artistId}", (string userName, int artistId, IUserRepository userFunctions) =>
            {// Kanske bör vara UserHandler userHanlder istället för IPostUserFunctions, inte testat än.
                userFunctions.ConnectUserToArtist(userName, artistId);
                return Results.Ok("Successfully connected user to artist.");
            });

            app.MapPost("/users/{userId}/genres/{genreId}", (int userId, int genreId, IUserRepository userFunctions) =>
            {// Kanske bör vara UserHandler userHanlder istället för IPostUserFunctions, inte testat än.
                userFunctions.ConnectUserToGenre(userId, genreId);
                return Results.Ok("Successfully connected user to genre.");
            });

            app.MapPost("/users/removeuser/{userName}", (IUserRepository userFunctions, string userName) =>
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
