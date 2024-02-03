using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    internal class FavoriteSongsScreen : ScreenBase
    {
        public FavoriteSongsScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task ListFavoriteSongsAsync(int userId)
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Your favorite songs ~~~~\n");

            try
            {
                var songList = await ApiService.GetUserFavoriteSongsAsync(userId);

                if (songList.Any())
                {
                    songList.OrderBy(s => s.SongTitle);

                    foreach (var song in songList)
                    {
                        Console.WriteLine($"Title: \t{song.SongTitle}");
                        Console.WriteLine($"Artist: {song.Artist}");
                        Console.Write("Genre: \t");
                        foreach (var genre in song.Genres)
                        {
                            Console.Write($"{genre}, ");
                        }
                        Console.WriteLine("\n");
                    }
                }
                else
                {
                    Console.WriteLine("No favorite songs found.");
                }
                // Wait for user to press ENTER key to return to main menu
                MenuHandler.ReturnToMainMenu();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error displaying favorite songs: {ex.Message}");
            }
        }
    }
}
