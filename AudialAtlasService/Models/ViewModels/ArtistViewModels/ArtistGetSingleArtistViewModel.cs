namespace AudialAtlasService.Models.ViewModels.ArtistViewModels
{
    public class ArtistGetSingleArtistViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Should be list of GenreViewModel
        public string[] Genres { get; set; }

        //// Should be list of SongViewModel
        //public virtual List<SongViewModel> Songs { get; set; }
    }
}
