using AudialAtlasService.Models;
using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Models.DTOs;
using AudialAtlasServiceClient.Models.ViewModels;
using AudialAtlasServiceClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Screens
{
    internal class ListAllSongsInDbScreen : ScreenBase
    {
        public ListAllSongsInDbScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task ListAllSongsInDbAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Songs ~~~~\n");

            List<ListAllSongsInDbViewModel> songList = await ApiService.GetAllSongsFromDb();

            if(songList.Count() == 0)
            {
                await Console.Out.WriteLineAsync("No songs found!");
                return;
            }

            foreach(ListAllSongsInDbViewModel song in songList)
            {
                await Console.Out.WriteLineAsync($"Song Id: {song.Id}");
                await Console.Out.WriteLineAsync($"Title: \t{song.SongTitle}");
                await Console.Out.WriteLineAsync($"Artist: {song.Artist}\n");
            }
            MenuHandler.ReturnToMainMenu();
        }
    }
}
