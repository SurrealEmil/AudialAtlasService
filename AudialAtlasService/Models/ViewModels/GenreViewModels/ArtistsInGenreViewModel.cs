namespace AudialAtlasService.Models.ViewModels.GenreViewModels
{
    public class ArtistsInGenreViewModel : GenreSingleViewModel
    {
        public virtual List<ArtistSingleViewModel> Artists { get; set; }
    }
}
