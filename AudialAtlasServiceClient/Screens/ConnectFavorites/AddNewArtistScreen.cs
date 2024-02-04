using AudialAtlasService.Models;
using AudialAtlasServiceClient.Models.DTOs.FavoritesDTO;
using AudialAtlasServiceClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Screens.ConnectFavorites
{
    internal class AddNewArtistScreen : ScreenBase
    {
        public AddNewArtistScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task AddNewFavoriteArtistAsync(int userId)
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Add new favorite artist ~~~~");

            Console.Write("\nPlease enter artist ID: ");
            int artistId = int.Parse(Console.ReadLine());

            try
            {
                AddFavoriteArtistDto addFavoriteArtistDto = new AddFavoriteArtistDto
                {
                    UserId = userId,
                    ArtistId = artistId
                };

                await ApiService.PostNewFavoriteArtistAsync(userId, addFavoriteArtistDto);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding new favorite artist: {ex.Message}");
            }
        }
    }
}
