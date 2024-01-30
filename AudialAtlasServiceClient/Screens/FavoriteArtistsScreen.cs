using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    internal class FavoriteArtistsScreen : ScreenBase
    {
        public FavoriteArtistsScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task ListFavoriteArtistsAsync(int userId)
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Your favorite artists ~~~~\n");

            try
            {
                var artistList = await ApiService.GetUserFavoriteArtistsAsync(userId);

                if (artistList.Any())
                {
                    foreach (var artist in artistList)
                    {
                        Console.WriteLine($"Artist: \t{artist.Name}");
                        Console.WriteLine($"Description: \t{artist.Description}\n");
                    }
                }
                else
                {
                    Console.WriteLine("No favorite artists found.");
                }
                // Wait for user to press ENTER key
                MenuHandler.ReturnToMainMenu();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error displaying favorite songs: {ex.Message}");
            }
        }
    }
}
