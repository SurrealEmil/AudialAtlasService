using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels.GenreViewModels;
using AudialAtlasService.Repositories;
using AudialAtlasService.Repositories.GenreRepoExceptions;
using System.Net;

namespace AudialAtlasService.Handlers
{
    public class GenreHandler
    {
        public static IResult ListAllGenre(IGenreRepository helper)
        {
            List<GenreListAllViewModel> list = helper.ListAllGenre();

            if (list == null)
            {
                return Results.NotFound(new { Message = "No genres found" });
            }

            return Results.Json(list);
        }

        public static IResult GetSingleGenre(IGenreRepository helper, int genreId)
        {
            GenreSingleViewModel? genre = helper.GetSingleGenre(genreId);

            if (genre == null)
            {
                return Results.NotFound(new { Message = $"No genre with id {genreId} found" });
            }

            return Results.Json(genre);
        }

        public static IResult GetAllArtistsInGenre(IGenreRepository helper, int genreId)
        {
            List<ArtistsInGenreViewModel> genre = helper.GetAllArtistsInGenre(genreId);

            if (genre == null)
            {
                return Results.NotFound(new { Message = $"No genre with id {genreId} found" });
            }

            return Results.Json(genre);
        }

        public static IResult GetAllSongsInGenre(IGenreRepository helper, int genreId)
        {
            List<SongsInGenreViewModel> genre = helper.GetAllSongsInGenre(genreId);

            if (genre == null)
            {
                return Results.NotFound(new { Message = $"No genre with id {genreId} found" });
            }

            return Results.Json(genre);
        }

        public static IResult PostGenre(IGenreRepository helper, GenreDto dto)
        {
            if (dto.GenreTitle == null)
            {
                return Results.BadRequest(new { Message = "GenreTitle field is required" });
            }

            try
            {
                helper.PostGenre(dto);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (GenreAlreadyExistsException ex)
            {
                // Handle GenreAlreadyExistsException if needed
                return Results.BadRequest(new { ex.Message });
            }
            catch (GenreSaveException ex)
            {
                // Handle GenreSaveException if needed
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
