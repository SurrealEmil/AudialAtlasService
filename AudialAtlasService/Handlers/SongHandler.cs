using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using System.Net;

namespace AudialAtlasService.Handlers
{
    public class SongHandler
    {
        public static IResult ListAllSongs(ApplicationContext context)
        {
            List<SongListAllViewModel> list = context.Songs
                .Select(s => new SongListAllViewModel()
                {
                    SongTitle = s.SongTitle,
                    // Implement ArtistViewModel
                })
                .ToList();

            return Results.Json(list);
        }

        public static IResult GetSingleSong(ApplicationContext context, int songId)
        {
            SongSingleViewModel? song = context.Songs
                .Where(s => s.SongId == songId)
                .Select(s => new SongSingleViewModel()
                {
                    SongTitle = s.SongTitle,
                })
                .SingleOrDefault();

            if(song == null)
            {
                return Results.NotFound(new { Message = "No song found" });
            }

            return Results.Json(song);
        }

        public static IResult PostSong(ApplicationContext context, SongDto dto)
        {
            Song? song = new Song()
            {
                SongTitle = dto.SongTitle
            };

            if(song == null)
            {
                return Results.BadRequest(new { Message = "SongTitle field is required" });
            }

            try
            {
                context.Songs.Add(song);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Results.Conflict(new { Message = $"Failed to create new song with error message: {ex.Message}" });
            }

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
