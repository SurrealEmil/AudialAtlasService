using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels.ArtistViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AudialAtlasService.DbHelpers
{
    public interface IArtistsDbHelper
    {
        public List<ArtistListAllViewModel> ArtistListAll();
        public ArtistGetSingleArtistViewModel GetSingleArtist(int artistId);
        public int AddArtistToDb(ArtistDto dto);
        public int LinkGenreToArtist(int artistId, int genreId);
    }

    public class ArtistRepository : IArtistsDbHelper
    {
        private static ApplicationContext _context;
        public ArtistRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<ArtistListAllViewModel> ArtistListAll()
        {
            List<Artist> artists = _context.Artists
                .Include(a => a.Genres)
                .ToList();

            List<ArtistListAllViewModel> result = artists
                .Select(a => new ArtistListAllViewModel()
                {
                    ArtistId = a.ArtistId,
                    Name = a.Name,
                    Genres = a.Genres
                        .Select(g => g.GenreTitle)
                        .ToArray()
                })
                .ToList();

            return result;
        }

        public ArtistGetSingleArtistViewModel GetSingleArtist(int artistId)
        {
            ArtistGetSingleArtistViewModel? artResult = _context.Artists
                .Where(a => a.ArtistId == artistId)
                .Select(a => new ArtistGetSingleArtistViewModel()
                {
                    Name = a.Name,
                    Description = a.Description,
                    Genres = a.Genres
                        .Select(g => g.GenreTitle)
                        .ToArray()
                })
                .SingleOrDefault();

            return artResult;
        }

        public int AddArtistToDb(ArtistDto dto)
        {
            // Maybe change return type to tuple? So method returns both string for error message and int for the switch.
            List<Artist> checkName = _context.Artists
                .Where(a => a.Name == dto.Name)
                .ToList();

            foreach (Artist check in checkName)
            {
                if (check.Description == dto.Description)
                {
                    // 1 means artist already exists
                    return 1;
                }
            }

            Artist artist = new Artist()
            {
                Name = dto.Name,
                Description = dto.Description
            };

            try
            {
                _context.Artists.Add(artist);
                _context.SaveChanges();
                // 0 means success
                return 0;
            }
            catch (Exception ex)
            {
                // -1 means method failed (Uses default in the switch).
                return -1;
            }
        }

        public int LinkGenreToArtist(int artistId, int genreId)
        {
            Artist? artist = _context.Artists
                .Where(a => a.ArtistId == artistId)
                .Include(a => a.Genres)
                .SingleOrDefault();
            if (artist == null)
            {
                return 0;
            }

            Genre? genre = _context.Genres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Artists)
                .SingleOrDefault();
            if (genre == null)
            {
                return 1;
            }

            try
            {
                artist.Genres.Add(genre);
                _context.Artists.Update(artist);
                _context.SaveChanges();
                return 2;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

    }
}
