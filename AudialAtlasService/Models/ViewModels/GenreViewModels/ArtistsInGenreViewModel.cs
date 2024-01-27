using AudialAtlasService.Models.ViewModels.ArtistViewModels;

namespace AudialAtlasService.Models.ViewModels.GenreViewModels
{
    public class ArtistsInGenreViewModel
    {
        public string GenreTitle { get; set; }
        public virtual List<ArtistSingleViewModel> Artists { get; set; }
    }
}
