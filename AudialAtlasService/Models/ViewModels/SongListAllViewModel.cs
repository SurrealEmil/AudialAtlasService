namespace AudialAtlasService.Models.ViewModels
{
    public class SongListAllViewModel
    {
        public int Id { get; set; }
        public string SongTitle { get; set; }

        // ArtistViewModel or only artist name
        public string? Artist { get; set; }
    }
}
