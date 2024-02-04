using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels.ArtistViewModels;
using AudialAtlasService.Models.ViewModels.GenreViewModels;
using AudialAtlasService.Repositories.GenreRepoExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace AudialAtlasService.Repositories
{
    public interface IGenreRepository
    {
        public List<GenreListAllViewModel> ListAllGenre();
        public GenreSingleViewModel GetSingleGenre(int genreId);
        public List<ArtistsInGenreViewModel> GetAllArtistsInGenre(int genreId);
        public List<SongsInGenreViewModel> GetAllSongsInGenre(int genreId);
        public void PostGenre(GenreDto dto);

    }

    public class GenreRepository : IGenreRepository
    {
        private static ApplicationContext _context;
        public GenreRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<GenreListAllViewModel> ListAllGenre()
        {
            List<Genre> genre = _context.Genres
                .ToList();

            List<GenreListAllViewModel> result = genre
                .Select(g => new GenreListAllViewModel()
                {
                    GenreId = g.GenreId,
                    GenreTitle = g.GenreTitle,
                })
                .ToList();

            return result;
        }

        public GenreSingleViewModel GetSingleGenre(int genreId)
        {
            GenreSingleViewModel? genre = _context.Genres
                .Where(g => g.GenreId == genreId)
                .Select(g => new GenreSingleViewModel()
                {
                    GenreTitle = g.GenreTitle,
                })
                .SingleOrDefault();

            return genre;
        }

        public List<ArtistsInGenreViewModel> GetAllArtistsInGenre(int genreId)
        {
            List<Genre> genre = _context.Genres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Artists)
                .ToList();

            List<ArtistsInGenreViewModel> result = genre
                .Select(g => new ArtistsInGenreViewModel
                {
                    GenreTitle = g.GenreTitle,
                    Artists = g.Artists
                    .Select(a => new ArtistSingleViewModel
                    {
                        Name = a.Name,
                        Description = a.Description,
                    }).ToList()
                })
                .ToList();

            return result;
        }

        public List<SongsInGenreViewModel> GetAllSongsInGenre(int genreId)
        {
            Genre? genre = _context.Genres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Songs)
                    .ThenInclude(s => s.Artist)  // Include the Artist for each Song
                .SingleOrDefault();

            if (genre == null || genre.Songs == null)
            {
                // Handle the case when the genre or songs are not found
                return new List<SongsInGenreViewModel>();
            }

            List<SongsInGenreViewModel> result = new List<SongsInGenreViewModel>
        {
            new SongsInGenreViewModel
            {
                GenreTitle = genre.GenreTitle,
                Songs = genre.Songs
                    .Select(song => new SongViewModel
                    {
                        SongTitle = song.SongTitle,
                        Artist = song.Artist?.Name ?? "Unknown Artist", // Handle potential null reference
                    })
                    .ToList()
            }
        };

                return result;
            }


        public void PostGenre(GenreDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (_context.Genres.Any(g => g.GenreTitle == dto.GenreTitle))
            {
                throw new GenreAlreadyExistsException($"Genre with title '{dto.GenreTitle}' already exists.");
            }

            Genre? genre = new Genre()
            {
                GenreTitle = dto.GenreTitle
            };

            try
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new GenreSaveException("Error while saving genre.", ex);
            }
        }
    }
}
