using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels.ArtistViewModels;
using AudialAtlasService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace AudialAtlasService.Handlers
{


    public class ArtistHandler
    {
        public static IResult GetAllArtists(IArtistsDbHelper helper)
        {
            List<ArtistListAllViewModel> list = helper.ArtistListAll();

            if (list == null)
            {
                return Results.NotFound(new { Message = "No artists found" });
            }

            return Results.Json(list);
        }

        public static IResult GetSingleArtist(IArtistsDbHelper helper, int artistId) 
        {
            ArtistGetSingleArtistViewModel? artist = helper.GetSingleArtist(artistId);

            if(artist == null)
            {
                return Results.NotFound(new { Message = $"No artist with id {artistId} found" });
            }

            return Results.Json(artist);
        }

        public static IResult PostArtist(IArtistsDbHelper helper, ArtistDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                return Results.BadRequest(new { Message = "Name field is required" });
            }
            if (string.IsNullOrEmpty(dto.Description))
            {
                return Results.BadRequest(new { Message = "Description field is required" });
            }

            int created = helper.AddArtistToDb(dto);

            switch(created)
            {
                case 0:
                    return Results.StatusCode((int)HttpStatusCode.Created);
                case 1:
                    return Results.BadRequest(new { Message = "Artist already exists" });
                default:
                    return Results.Conflict(new { Message = "Failed to add artist to database" });
            }
        }

        public static IResult LinkGenreToArtist(IArtistsDbHelper helper, int artistId, int genreId)
        {
            int linkArtistAndGenre = helper.LinkGenreToArtist(artistId, genreId);

            switch (linkArtistAndGenre)
            {
                case 0:
                    return Results.NotFound(new { Message = $"No artist with id {artistId} found" });
                case 1:
                    return Results.NotFound(new { Message = $"No genre with id {genreId} found" });
                case 2:
                    return Results.StatusCode((int)HttpStatusCode.Created);
                default:
                    return Results.Conflict(new { Message = "Failed to add genre to artist" });
            }
        }
    }
}
