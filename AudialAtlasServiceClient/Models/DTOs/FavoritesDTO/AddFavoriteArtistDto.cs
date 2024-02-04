using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Models.DTOs.FavoritesDTO
{
    public class AddFavoriteArtistDto
    {
        public int UserId { get; set; }
        public int ArtistId { get; set; }
    }
}
