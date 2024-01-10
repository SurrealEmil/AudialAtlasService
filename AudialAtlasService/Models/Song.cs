namespace AudialAtlasService.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string SongTitle { get; set; }
        public string Artist { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        public virtual Artist Artists { get; set; }
    }
}
