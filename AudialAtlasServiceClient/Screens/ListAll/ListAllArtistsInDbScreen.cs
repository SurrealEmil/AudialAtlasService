using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Models.ViewModels.ListAllView;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens.ListAll
{
    internal class ListAllArtistsInDbScreen : ScreenBase
    {
        public ListAllArtistsInDbScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task ListAllArtistsInDbAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Artists ~~~~\n");

            List<ListAllArtistsInDbViewModel> artistList = await ApiService.GetAllArtistsFromDbAsync();

            if (artistList.Count() == 0)
            {
                await Console.Out.WriteLineAsync("No songs found!");
                return;
            }

            artistList.OrderBy(a => a.Name);

            foreach (ListAllArtistsInDbViewModel artist in artistList)
            {
                await Console.Out.WriteLineAsync($"Id: \t{artist.ArtistId}");
                await Console.Out.WriteLineAsync($"Title: \t{artist.Name}\n");
            }
            MenuHandler.ReturnToMainMenu();
        }
    }
}
