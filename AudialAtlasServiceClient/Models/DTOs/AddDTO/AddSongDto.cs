using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models;
using AudialAtlasService.Repositories.SongRepoExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Models.DTOs.AddDTO
{
    public class AddSongDto
    {
        public string SongTitle { get; set; }
    }
}
