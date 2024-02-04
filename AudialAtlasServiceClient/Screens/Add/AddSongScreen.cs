using AudialAtlasServiceClient.Models.DTOs.AddDTO;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens.Add
{
    internal class AddSongScreen : ScreenBase
    {
        public AddSongScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task AddSongAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Add new song ~~~~");

            Console.Write("\nPlease enter artist ID: ");
            int artistId = int.Parse(Console.ReadLine());

            Console.Write("\nPlease enter genre ID: ");
            int genreId = int.Parse(Console.ReadLine());

            Console.Write("\nPlease enter song: ");
            string songTitle = Console.ReadLine();

            try
            {
                AddSongDto addSongDto = new AddSongDto
                {
                    SongTitle = songTitle,
                };

                await ApiService.PostNewSongAsync(artistId, genreId, addSongDto);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding new song: {ex.Message}");
            }
        }
    }

}
