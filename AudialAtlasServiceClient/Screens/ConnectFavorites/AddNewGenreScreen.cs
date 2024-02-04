using AudialAtlasServiceClient.Models.DTOs.FavoritesDTO;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens.ConnectFavorites
{
    internal class AddNewGenreScreen : ScreenBase
    {
        public AddNewGenreScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task AddNewFavoriteGenreAsync(int userId)
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Add new favorite genre ~~~~");

            Console.Write("\nPlease enter genre ID: ");
            int genreId = int.Parse(Console.ReadLine());

            try
            {
                AddFavoriteGenreDto addFavoriteGenreDto = new AddFavoriteGenreDto
                {
                    UserId = userId,
                    GenreId = genreId
                };

                await ApiService.PostNewFavoriteGenreAsync(userId, addFavoriteGenreDto);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding new favorite artist: {ex.Message}");
            }
        }
    }
}
