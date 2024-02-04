using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Models.ViewModels.ListAllView;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens.ListAll
{
    internal class ListAllSongsInDbScreen : ScreenBase
    {
        public ListAllSongsInDbScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task ListAllSongsInDbAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Songs ~~~~\n");

            List<ListAllSongsInDbViewModel> songList = await ApiService.GetAllSongsFromDbAsync();

            if (songList.Count() == 0)
            {
                await Console.Out.WriteLineAsync("No songs found!");
                return;
            }

            songList.OrderBy(s => s.SongTitle);

            foreach (ListAllSongsInDbViewModel song in songList)
            {
                await Console.Out.WriteLineAsync($"Id: \t{song.Id}");
                await Console.Out.WriteLineAsync($"Title: \t{song.SongTitle}");
                await Console.Out.WriteLineAsync($"Artist: {song.Artist}\n");
            }
            MenuHandler.ReturnToMainMenu();
        }
    }
}
