using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Models.ViewModels
{
    public class ListAllSongsInDbViewModel
    {
        public int Id { get; set; }
        public string SongTitle { get; set; }
        public string? Artist { get; set; }
    }
}
