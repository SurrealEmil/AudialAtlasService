using AudialAtlasService.Models.ViewModels;
using AudialAtlasServiceClient.Services;
using Newtonsoft.Json;

namespace AudialAtlasServiceClient.Screens
{
    internal class FavoriteSongsScreen : ScreenBase
    {
        public FavoriteSongsScreen(string apiBaseUrl) : base(apiBaseUrl) {}

        public async Task ListFavoriteSongsAsync()
        {
            try
            {
                var songList = await ApiService.GetAllSongsAsync();

                    if (songList.Any())
                    {
                        foreach (var song in songList)
                        {
                            //Console.WriteLine($"Title: {song.SongTitle}");
                            //Console.WriteLine($"Artist: {song.ArtistName}");
                            //Console.WriteLine($"Genre: {song.GenreTitle}");
                            Console.WriteLine(); // Add spacing before the next song
                        }
                    }
                    else
                    {
                        Console.WriteLine("No favorite songs found.");
                    }

                    // Wait for user to press ENTER key
                    Console.WriteLine("\nPress ENTER to return to the main menu.");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                    Console.WriteLine("\nReturning to main menu...");
                    Thread.Sleep(1000);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error displaying favorite songs: {ex.Message}");
            }
        }
    }
}
