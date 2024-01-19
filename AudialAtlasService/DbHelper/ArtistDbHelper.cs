using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.ViewModels.ArtistViewModels;
using Microsoft.EntityFrameworkCore;

namespace AudialAtlasService.DbHelper
{
    public interface IGetArtistsDbHelper
    {
        public static List<ArtistListAllViewModel> ArtistListAll();
    }

    public class ArtistDbHelper : IGetArtistsDbHelper
    {
        private static ApplicationContext _context;
        public ArtistDbHelper(ApplicationContext context)
        {
            _context = context;
        }

        public static List<ArtistListAllViewModel> ArtistListAll()
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
    }
}
