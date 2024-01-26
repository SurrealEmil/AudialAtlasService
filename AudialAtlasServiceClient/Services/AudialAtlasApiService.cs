using AudialAtlasService.Models.ViewModels;
using AudialAtlasService.Models.ViewModels.ArtistViewModels;
using Newtonsoft.Json;

namespace AudialAtlasServiceClient.Services
{
    public class AudialAtlasApiService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string _apiBaseUrl;

        public AudialAtlasApiService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
        }

        public async Task<List<ArtistListAllViewModel>> GetAllSongsAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{_apiBaseUrl}/songs");

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                var songList = JsonConvert.DeserializeObject<List<ArtistListAllViewModel>>(result);

                return songList ?? new List<ArtistListAllViewModel>();
            }            

            throw new Exception("Failed to get songs.");
        }
    }
}