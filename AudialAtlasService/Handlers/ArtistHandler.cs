using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels.ArtistViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace AudialAtlasService.Handlers
{


    public class ArtistHandler
    {


        public static IResult GetAllArtists(ApplicationContext context)
        {
            List<Artist> artists = context.Artists
                .Include(a => a.Genres)
                .ToList();

            List<ArtistListAllViewModel> result = new List<ArtistListAllViewModel>();

            foreach(Artist artist in artists)
            {
                ArtistListAllViewModel addToResult = new ArtistListAllViewModel()
                {
                    ArtistId = artist.ArtistId,
                    Name = artist.Name,
                    Genres = artist.Genres
                        .Select(g => g.GenreTitle)
                        .ToArray()
                };
                result.Add(addToResult);
            }

            return Results.Json(result);
        }

        public static IResult GetSingleArtist(ApplicationContext context, int artistId) 
        {
            Artist? artist = context.Artists
                .Where(a => a.ArtistId == artistId)
                .Include(a => a.Genres)
                .SingleOrDefault();

            if (artist == null)
            {
                return Results.NotFound(new { Message = "No artist found" });
            }

            ArtistGetSingleArtistViewModel? artResult = new ArtistGetSingleArtistViewModel()
            {
                Name = artist.Name,
                Description = artist.Description,
                Genres = artist.Genres
                    .Select(g => g.GenreTitle)
                    .ToArray()
            };

            return Results.Json(artResult);
        }

        public static IResult PostArtist(ApplicationContext context, ArtistDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                return Results.BadRequest(new { Message = "Name field is required" });
            }
            if (string.IsNullOrEmpty(dto.Description))
            {
                return Results.BadRequest(new { Message = "Description field is required" });
            }

            List<Artist> checkName = context.Artists
                .Where(a => a.Name == dto.Name)
                .ToList();

            foreach(Artist check in checkName)
            {
                if (check.Description == dto.Description)
                {
                    return Results.BadRequest(new { Message = "Combination of name and description already exists" });
                }
            }

            Artist artist = new Artist()
            {
                Name = dto.Name,
                Description = dto.Description
            };

            try
            {
                context.Artists.Add(artist);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Results.Conflict(new { Message = $"Request failed with error message {ex.Message}" });
            }

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult LinkGenreToArtist(ApplicationContext context, int artistId, int genreId)
        {
            Artist? artist = context.Artists
                .Where(a => a.ArtistId == artistId)
                .Include(a => a.Genres)
                .SingleOrDefault();
            if (artist == null)
            {
                return Results.NotFound(new { Message = $"No artist with id {artistId}" });
            }

            Genre? genre = context.Genres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Artists)
                .SingleOrDefault();
            if (genre == null)
            {
                return Results.NotFound(new { Message = $"No genre with id {genreId}" });
            }

            try
            {
                artist.Genres.Add(genre);
                context.Artists.Update(artist);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Conflict(new { Message = $"Failed to add genre to artist with error: {ex.Message}" });
            }

            
        }
    }
}
