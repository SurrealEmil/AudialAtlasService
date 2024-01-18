using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using AudialAtlasService.Models.ViewModels.ArtistViewModels;
using AudialAtlasService.Models.ViewModels.GenreViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace AudialAtlasService.Handlers
{
    public class GenreHandler
    {
        public static IResult ListAllGenre(ApplicationContext context)
        {
            GenreListAllViewModel result = new GenreListAllViewModel
            {
                Genres = context.Genres
                    .Select(g => new GenreSingleViewModel()
                    {
                        GenreId = g.GenreId,
                        GenreTitle = g.GenreTitle,
                    })
                    .ToList()
            };

            return Results.Json(result);
        }

        public static IResult GetSingleGenre(ApplicationContext context, [FromRoute] int genreId)
        {
            GenreSingleViewModel? genre = context.Genres
                .Where(g => g.GenreId == genreId)
                .Select(g => new GenreSingleViewModel()
                {
                    GenreId = g.GenreId,
                    GenreTitle = g.GenreTitle,
                })
                .SingleOrDefault();

            if (genre == null)
            {
                return Results.NotFound(new { Message = "No genre found" });
            }

            return Results.Json(genre);
        }

        public static IResult GetAllArtistsInGenre(ApplicationContext context, int genreId)
        {
            ArtistsInGenreViewModel? genre = context.Genres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Artists)
                .Select(g => new ArtistsInGenreViewModel
                {
                    GenreId = g.GenreId,
                    GenreTitle = g.GenreTitle,
                    Artists = g.Artists.Select(a => new ArtistSingleViewModel
                    {
                        // Map artist properties as needed
                        Name = a.Name,
                        Description = a.Description,
                    }).ToList()
                })
                .SingleOrDefault();

            if (genre == null)
            {
                return Results.NotFound(new { Message = "No genre found" });
            }

            return Results.Json(genre);
        }

        public static IResult GetAllSongsInGenre(ApplicationContext context, int genreId)
        {
            SongsInGenreViewModel? genre = context.Genres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Songs)
                .Select(g => new SongsInGenreViewModel()
                {
                    GenreId = g.GenreId,
                    GenreTitle = g.GenreTitle,
                    Songs = g.Songs.Select(s => new SongSingleViewModel
                    {
                        // Map songs properties as needed
                        SongTitle = s.SongTitle,
                    }).ToList()
                })
                .SingleOrDefault();

            if (genre == null)
            {
                return Results.NotFound(new { Message = "No genre found" });
            }

            return Results.Json(genre);
        }

        public static IResult PostGenre(ApplicationContext context, GenreDto dto)
        {
            Genre? genre = new Genre()
            {
                GenreTitle = dto.GenreTitle
            };

            if (genre == null)
            {
                return Results.BadRequest(new { Message = "GenreTitle field is required" });
            }

            try
            {
                context.Genres.Add(genre);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Results.Conflict(new { Message = $"Failed to create new genre with error message: {ex.Message}" });
            }

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
