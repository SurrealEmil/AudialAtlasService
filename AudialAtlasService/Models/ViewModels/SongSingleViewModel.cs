namespace AudialAtlasService.Models.ViewModels
{
    public class SongSingleViewModel
    {
        public string SongTitle { get; set; }

        // ArtistViewModel or only artist name
        public virtual Artist Artist { get; set; }

        //Implement Genres with GenreViewModel
        //public virtual ICollection<GenreViewModel> Genres { get; set; }
    }
}
