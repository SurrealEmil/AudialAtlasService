namespace AudialAtlasService.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreTitle { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
