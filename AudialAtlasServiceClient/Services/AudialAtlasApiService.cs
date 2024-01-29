using AudialAtlasService.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AudialAtlasServiceClient.Services
{
    public interface IAudialAtlasApiService
    {
        Task<int> UserAuthentication(string username, string password);
        Task<List<SongListAllViewModel>> GetUserLikedSongsAsync(int userId);
    }

    public class AudialAtlasApiService : IAudialAtlasApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public AudialAtlasApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"];
        }

#region Add error handling for Http requests

        public async Task<int> UserAuthentication(string userName, string password)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/users/login/{userName}/{password}");

            if (response.IsSuccessStatusCode)
            {
                string authenticatedUser = await response.Content.ReadAsStringAsync();
                int authenticatedUserId = int.Parse(authenticatedUser);

                if (authenticatedUserId > 0)
                {
                    return authenticatedUserId;
                }
            }
            return -1; // Authentication failed
        }


        public async Task<List<SongListAllViewModel>> GetUserLikedSongsAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/users/{userId}/songs");

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                var songList = JsonConvert.DeserializeObject<List<SongListAllViewModel>>(result);

                return songList ?? new List<SongListAllViewModel>();
            }
            throw new Exception("Failed to get songs.");
        }


        //public async Task<List<ArtistListAllViewModel>> GetAllSongsAsync()
        //{
        //    HttpResponseMessage response = await httpClient.GetAsync($"{_apiBaseUrl}/users/{userId}/songs");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string result = await response.Content.ReadAsStringAsync();

        //        var songList = JsonConvert.DeserializeObject<List<ArtistListAllViewModel>>(result);

        //        return songList ?? new List<ArtistListAllViewModel>();
        //    }

        //    throw new Exception("Failed to get songs.");
        //}
    }
} 
#endregion