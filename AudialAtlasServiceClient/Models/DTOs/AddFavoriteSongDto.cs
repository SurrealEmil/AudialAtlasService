using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Models.DTOs
{
    public class AddFavoriteSongDto
    {
        public int UserId { get; set; }
        public int SongId { get; set; }
    }
}
