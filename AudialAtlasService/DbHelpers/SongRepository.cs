using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AudialAtlasService.DbHelpers
{
    public interface ISongDbHelper
    {
        public List<SongListAllViewModel> ListAllSongs();
        public SongSingleViewModel GetSingleSong(int songId);
        public int PostSong(int artistId, SongDto dto);

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

            return song;
        }

        public int PostSong(int artistId, SongDto dto)
        {
            Artist? artist = _context.Artists
                .Where(a => a.ArtistId == artistId)
                .Include(a => a.Songs)
                .SingleOrDefault();
            if(artist == null) 
            {
                return 0;
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
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
