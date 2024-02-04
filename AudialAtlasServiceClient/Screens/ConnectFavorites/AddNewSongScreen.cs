using AudialAtlasServiceClient.Models.DTOs.FavoritesDTO;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens.ConnectFavorites
{
    internal class AddNewSongScreen : ScreenBase
    {
        public AddNewSongScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task AddNewFavoriteSongAsync(int userId)
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Add new favorite song ~~~~");

            Console.Write("\nPlease enter song ID: ");
            int songId = int.Parse(Console.ReadLine());

            try
            {
                AddFavoriteSongDto addFavoriteSongDto = new AddFavoriteSongDto
                {
                    UserId = userId,
                    SongId = songId
                };

                await ApiService.PostNewFavoriteSongAsync(userId, addFavoriteSongDto);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding new favorite song: {ex.Message}");
            }
        }
    }
}
