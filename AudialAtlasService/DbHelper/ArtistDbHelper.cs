using AudialAtlasService.Data;

namespace AudialAtlasService.DbHelper
{
    public interface IArtistHandler
    {
        
    }

    public class ArtistDbHelper
    {
        private static ApplicationContext _context;
        public ArtistDbHelper(ApplicationContext context)
        {
            _context = context;
        }



    }
}
