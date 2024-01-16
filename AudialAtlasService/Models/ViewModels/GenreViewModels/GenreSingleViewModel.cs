using AudialAtlasService.Models.ViewModels.ArtistViewModels;

namespace AudialAtlasService.Models.ViewModels.GenreViewModels
{
    public class GenreSingleViewModel
    {
        public int GenreId { get; set; }
        public string GenreTitle { get; set; }
        public virtual ICollection<ArtistListAllViewModel> Artists { get; set; }
    }
}
