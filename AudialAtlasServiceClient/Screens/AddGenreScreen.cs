using AudialAtlasServiceClient.Models.DTOs;
using AudialAtlasServiceClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Screens
{
    internal class AddGenreScreen : ScreenBase
    {
        public AddGenreScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task AddGenreAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Add new genre ~~~~");

            Console.Write("\nPlease enter genre: ");
            string genreTitle = Console.ReadLine();

            try
            {
                AddGenreDto addGenreDto = new AddGenreDto
                {
                    GenreTitle = genreTitle
                };

                await ApiService.PostNewGenreAsync(addGenreDto);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding new genre: {ex.Message}");
            }
        }
    }
}
