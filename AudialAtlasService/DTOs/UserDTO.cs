using AudialAtlasService.Data;
using AudialAtlasService.Models;

namespace AudialAtlasService.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

    }
}
