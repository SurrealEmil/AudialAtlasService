using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Models.ViewModels;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    internal class GetTopFiveSongsOfArtistScreen : ScreenBase
    {
        public GetTopFiveSongsOfArtistScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task ListFavoriteSongsAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Top Five Songs of Artist ~~~~\n");

            try
            {
                Console.Write("Artist: ");
                string? searchArtistQuery = Console.ReadLine();
                List<TopFiveSongsOfArtistViewModel> resultList = new List<TopFiveSongsOfArtistViewModel>();

                try
                {
                    resultList = await ApiService.GetTopFiveSongsOfArtistAsync(searchArtistQuery);
                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync($"Failed to get songs from artist {searchArtistQuery} with error message: {e.Message}");
                }

                if (resultList.Count == 0)
                {
                    await Console.Out.WriteLineAsync($"No songs found with search query {searchArtistQuery}");
                }
                else
                {
                    await Console.Out.WriteLineAsync($"Top five songs for artist {searchArtistQuery}\n");

                    foreach (TopFiveSongsOfArtistViewModel song in resultList)
                    {
                        await Console.Out.WriteLineAsync($"\t{song.Title}\n");
                    }
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
