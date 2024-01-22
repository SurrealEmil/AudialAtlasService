using AudialAtlasService.Data;
using AudialAtlasService.DbHelpers;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using System.Net;

namespace AudialAtlasService.Handlers
{
    public class SongHandler
    {
        public static IResult ListAllSongs(ISongDbHelper helper)
        {
            List<SongListAllViewModel> result = helper.ListAllSongs();

            return Results.Json(result);
        }

        public static IResult GetSingleSong(ISongDbHelper helper, int songId)
        {
            SongSingleViewModel? song = helper.GetSingleSong(songId);

            if(song == null)
            {
                return Results.NotFound(new { Message = $"No song with id {songId} found" });
            }

            return Results.Json(song);
        }

        public static IResult PostSong(ISongDbHelper helper, int artistId, SongDto dto)
        {
            if(dto.SongTitle == null)
            {
                return Results.BadRequest(new {Message = "Song title is required"});
            }

            int created = helper.PostSong(artistId, dto);

            switch (created)
            {
                case 0:
                    return Results.NotFound(new { Message = $"No artist with id {artistId} found" });
                case 1:
                    return Results.StatusCode((int)HttpStatusCode.Created);
                default:
                    return Results.Conflict(new { Message = "Failed to add song to artist" });
            }
        }

        public static IResult LinkGenreToSong(ISongDbHelper helper, int songId, int genreId)
        {
            int linkGenreToSong = helper.LinkGenreToSong(songId, genreId);

            switch (linkGenreToSong)
            {
                case 0:
                    return Results.NotFound(new { Message = $"No song with id {songId} found" });
                case 1:
                    return Results.NotFound(new {Message = $"No genre with id {genreId} found" });
                case 2:
                    return Results.StatusCode((int)HttpStatusCode.Created);
                default:
                    return Results.Conflict(new { Message = "Failed to link genre to song" });
            }
        }
    }
}
