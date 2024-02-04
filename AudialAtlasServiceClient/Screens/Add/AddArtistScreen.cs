using AudialAtlasServiceClient.Models.DTOs.AddDTO;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens.Add
{
    internal class AddArtistScreen : ScreenBase
    {
        public AddArtistScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task AddArtistAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Add new artist ~~~~");

            Console.Write("\nPlease enter name: ");
            string name = Console.ReadLine();

            Console.Write("\nPlease enter description: ");
            string description = Console.ReadLine();

            try
            {
                AddArtistDto addArtistDto = new AddArtistDto
                {
                    Name = name,
                    Description = description
                };

                await ApiService.PostNewArtistAsync(addArtistDto);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding new artist: {ex.Message}");
            }
        }
    }
}
