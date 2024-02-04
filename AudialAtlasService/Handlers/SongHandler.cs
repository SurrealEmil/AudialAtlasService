using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using AudialAtlasService.Repositories;
using AudialAtlasService.Repositories.SongRepoExceptions;
using System.Net;

namespace AudialAtlasService.Handlers
{
    public class SongHandler
    {
        public static IResult ListAllSongs(ISongRepository helper)
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

        public static IResult GetSingleSong(ISongRepository helper, int songId)
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

        public static IResult PostSong(ISongRepository helper, int artistId, int genreId, SongDto dto)
        {
            if (dto.SongTitle == null)
            {
                return Results.BadRequest(new { Message = "Song title is required" });
            }

            try
            {
                helper.PostSong(artistId, genreId, dto);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (SongFailedToAddToDatabaseException e)
            {
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        public static IResult LinkGenreToSong(ISongRepository helper, int songId, int genreId)
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
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
