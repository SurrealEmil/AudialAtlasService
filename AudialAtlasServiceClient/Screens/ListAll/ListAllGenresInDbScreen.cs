using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Models.ViewModels.ListAllView;
using AudialAtlasServiceClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Screens.ListAll
{
    public class ListAllGenresInDbScreen : ScreenBase
    {
        public ListAllGenresInDbScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task ListAllGenresInDbAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Genres ~~~~\n");

            List<ListAllGenresInDbViewModel> genresList = await ApiService.GetAllGenresFromDbAsync();

            if (genresList.Count() == 0)
            {
                await Console.Out.WriteLineAsync("No songs found!");
                return;
            }

            genresList.OrderBy(g => g.GenreTitle);

            foreach (ListAllGenresInDbViewModel genre in genresList)
            {
                await Console.Out.WriteLineAsync($"Id: \t{genre.GenreId}");
                await Console.Out.WriteLineAsync($"Title: \t{genre.GenreTitle}\n");
            }
            MenuHandler.ReturnToMainMenu();
        }
    }
}
