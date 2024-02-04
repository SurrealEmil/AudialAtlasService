using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels.ArtistViewModels;
using Microsoft.EntityFrameworkCore;

namespace AudialAtlasService.Repositories
{
    public interface IArtistRepository
    {
        public List<ArtistListAllViewModel> ArtistListAll();
        public ArtistGetSingleArtistViewModel GetSingleArtist(int artistId);
        public void AddArtistToDb(ArtistDto dto);
        public int LinkGenreToArtist(int artistId, int genreId);
    }

    public class ArtistRepository : IArtistRepository
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
                        .ToArray(),
                    Songs = a.Songs
                        .Select(s => s.SongTitle)
                        .ToArray()
                })
                .SingleOrDefault();

            return artResult;
        }

        public void AddArtistToDb(ArtistDto dto)
        {
            // Maybe change return type to tuple? So method returns both string for error message and int for the switch.
            List<Artist> checkName = _context.Artists
                .Where(a => a.Name == dto.Name)
                .ToList();

            foreach (Artist check in checkName)
            {
                if (check.Description == dto.Description)
                {
                    throw new ArgumentException();
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
            }
            catch
            {
                throw new Exception();
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
