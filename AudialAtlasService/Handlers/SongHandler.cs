using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using AudialAtlasService.Repositories;
using AudialAtlasService.Repositories.SongRepoExceptions;
using System.Net;

namespace AudialAtlasService.Handlers
{
    public class SongHandler
    {
        public static IResult ListAllSongs(ISongDbHelper helper)
        {
            try
            {
                List<SongListAllViewModel> result = helper.ListAllSongs();
                return Results.Json(result);
            }
            catch (SongNotFoundException e)
            {
                return Results.NotFound(e);
            }
        }

        public static IResult GetSingleSong(ISongDbHelper helper, int songId)
        {
            try
            {
                SongSingleViewModel? song = helper.GetSingleSong(songId);
                return Results.Json(song);
            }
            catch (SongNotFoundException)
            {
                return Results.NotFound(new { Message = $"No song with id {songId} found" });
            }
        }

        public static IResult PostSong(ISongDbHelper helper, int artistId, SongDto dto)
        {
            if(dto.SongTitle == null)
            {
                return Results.BadRequest(new {Message = "Song title is required"});
            }

            try
            {
                helper.PostSong(artistId, dto);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (SongFailedToAddToDatabaseException e)
            {
                return Results.Conflict(new
                {
                    Message = $"Failed with message: {e.Message}"
                });
            }
        }

        public static IResult LinkGenreToSong(ISongDbHelper helper, int songId, int genreId)
        {
            try
            {
                helper.LinkGenreToSong(songId, genreId);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (SongNotFoundException e)
            {
                return Results.NotFound(new { Message = $"Failed with message: {e.Message}" });
            }
            catch (SongFailedToAddToDatabaseException e)
            {
                return Results.Conflict(new { Message = $"Failed with message: {e.Message}" });
            }
        }
    }
}
