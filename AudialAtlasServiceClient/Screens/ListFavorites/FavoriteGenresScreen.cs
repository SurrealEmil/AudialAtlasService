using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens.ListFavorites
{
    internal class FavoriteGenresScreen : ScreenBase
    {
        public FavoriteGenresScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task ListFavoriteGenresAsync(int userId)
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Your favorite genres ~~~~\n");

            try
            {
                var genresList = await ApiService.GetUserFavoriteGenresAsync(userId);

                if (genresList.Any())
                {
                    genresList.OrderBy(g => g.GenreTitle);

                    foreach (var genre in genresList)
                    {
                        Console.WriteLine($"{genre.GenreTitle}\n");
                    }
                }
                else
                {
                    Console.WriteLine("No favorite genres found.");
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
