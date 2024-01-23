using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using AudialAtlasService.Repositories.SongRepoExceptions;
using Microsoft.EntityFrameworkCore;

namespace AudialAtlasService.Repositories
{
    public interface ISongDbHelper
    {
        public List<SongListAllViewModel> ListAllSongs();
        public SongSingleViewModel GetSingleSong(int songId);
        public void PostSong(int artistId, SongDto dto);
        public void LinkGenreToSong(int songId, int genreId);
    }

    public class SongRepository : ISongDbHelper
    {
        private ApplicationContext _context;
        public SongRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<SongListAllViewModel> ListAllSongs()
        {
            List<SongListAllViewModel> list = _context.Songs
                .Select(s => new SongListAllViewModel()
                {
                    SongTitle = s.SongTitle,
                    Artist = s.Artist.Name
                })
                .ToList();

            if(list.Count <= 0)
            {
                throw new SongNotFoundException("No songs in database");
            }
            return list;
        }

        public SongSingleViewModel GetSingleSong(int songId)
        {
            SongSingleViewModel? song = _context.Songs
                .Where(s => s.SongId == songId)
                .Select(s => new SongSingleViewModel()
                {
                    SongTitle = s.SongTitle,
                    Artist = s.Artist.Name,
                    Genres = s.Genres
                        .Select(g => g.GenreTitle)
                        .ToArray()
                })
                .SingleOrDefault();

            if(song == null)
            {
                throw new SongNotFoundException();
            }

            return song;
        }

        public void PostSong(int artistId, SongDto dto)
        {
            Artist? artist = _context.Artists
                .Where(a => a.ArtistId == artistId)
                .Include(a => a.Songs)
                .SingleOrDefault();
            if(artist == null) 
            {
                throw new SongFailedToAddToDatabaseException($"No artist with id {artistId} found");
            }

            Song? song = new Song()
            {
                SongTitle = dto.SongTitle,
                Artist = artist
            };

            try
            {
                _context.Songs.Add(song);
                _context.SaveChanges();
            }
            catch
            {
                throw new SongFailedToAddToDatabaseException();
            }
        }

        public void LinkGenreToSong(int songId, int genreId)
        {
            Song? song = _context.Songs
                .Where(s => s.SongId == songId)
                .Include(s => s.Genres)
                .SingleOrDefault();
            if(song == null)
            {
                throw new SongNotFoundException($"No song with id {songId} found");
            }

            Genre? genre = _context.Genres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Songs)
                .SingleOrDefault();
            if(genre == null) 
            { 
                // Placeholder for genre exception?
                throw new SongNotFoundException($"No genre with id {genreId} found");
            }

            try
            {
                song.Genres.Add(genre);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Change exception to more specifik exception.
                throw new SongFailedToAddToDatabaseException("Failed to add genre to song");
            }
        }
    }
}
