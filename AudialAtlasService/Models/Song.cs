﻿namespace AudialAtlasService.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string SongTitle { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
