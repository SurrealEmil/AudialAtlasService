using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using AudialAtlasService.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AudialAtlasService.Handlers
{
    public class UserHandler
    {
        public static IResult ConnectUserToArtistHandler([FromServices] IUserRepository userRepository, UserArtistConnectionDto connectionDto)
        {
            try
            {
                userRepository.ConnectUserToArtist(connectionDto);
                return Results.Ok("User successfully connected to artist.");
            }
            catch (KeyNotFoundException exKey)
            {
                return Results.NotFound(exKey.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, title: "Error connecting user to artist");
            }
        }

        public static IResult ConnectUserToGenreHandler([FromServices] IUserRepository userRepository, UserGenreConnectionDto connectionDto)
        {
            try
            {
                userRepository.ConnectUserToGenre(connectionDto);
                return Results.Ok("User successfully connected to genre.");
            }
            catch (KeyNotFoundException exKey)
            {
                return Results.NotFound(exKey.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, title: "Error connecting user to genre");
            }
        }

        public static IResult ConnectUserToSongHandler([FromServices] IUserRepository userRepository, UserSongConnectionDto connectionDto)
        {
            try
            {
                userRepository.ConnectUserToSong(connectionDto);
                return Results.Ok("User successfully connected to song.");
            }
            catch (KeyNotFoundException exKey)
            {
                return Results.NotFound(exKey.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, title: "Error connecting user to song");
            }
        }

        public static IResult GetAllArtistsLikedByUserHandler([FromServices] IUserRepository userFunctions, int userId)
        {
            Console.WriteLine("\nCONSOLE LOG\nINFO: GetAllArtistsLikedByUser HAS BEEN CALLED\n");

            var getArtistConnectedToUser = userFunctions.GetAllArtistsLikedByUser(userId);

            return getArtistConnectedToUser.Any()
                ? Results.Ok(getArtistConnectedToUser)
                : Results.NotFound("No artists liked by this user.");
        }

        public static IResult GetAllGenresLikedByUserHandler([FromServices] IUserRepository userRepository, int userId)
        {
            Console.WriteLine("\nCONSOLE LOG\nINFO: GetAllGenresLikedByUser HAS BEEN CALLED\n");

            var genresConnectedToUser = userRepository.GetAllGenresLikedByUser(userId);

            return genresConnectedToUser.Any()
                ? Results.Ok(genresConnectedToUser)
                : Results.NotFound("No genres found for this user.");
        }

        public static IResult GetAllSongsLikedByUserHandler([FromServices] IUserRepository userRepository, int userId)
        {
            Console.WriteLine("\nCONSOLE LOG\nINFO: GetAllSongsLikedByUser HAS BEEN CALLED\n");

            var songsConnectedToUser = userRepository.GetAllSongsLikedByUser(userId);

            return songsConnectedToUser.Any()
                ? Results.Ok(songsConnectedToUser)
                : Results.NotFound("No songs found for this user.");
        }

        public static IResult CheckIfUserExistsHandler([FromServices] IUserRepository userRepository, string userName)
        {
            Console.WriteLine("INFO: CheckIfUserExists HAS BEEN CALLED");
            try
            {
                bool userExists = userRepository.CheckIfUserExists(userName);
                return userExists
                    ? Results.Ok("User exists.")
                    : Results.NotFound("User does not exist.");
            }
            catch (Exception)
            {
                return Results.Problem("An unexpected error occurred.");
            }
        }

        public static IResult UserAuthentication([FromServices] IUserRepository userRepository, string userName, string password)
        {
            int authenticatedUserId = userRepository.AuthenticateUser(userName, password);

            return Results.Ok(authenticatedUserId);
        }

        public static IResult AddUser([FromServices] IUserRepository userRepository, UserDto dto)
        {
            try
            {
                userRepository.AddUser(dto);
            }
            catch (Exception)
            {
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
