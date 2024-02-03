using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Models.DTOs.FavoritesDTO
{
    internal class AddFavoriteGenreDto
    {
        public int UserId { get; set; }
        public int GenreId { get; set; }
    }
}
