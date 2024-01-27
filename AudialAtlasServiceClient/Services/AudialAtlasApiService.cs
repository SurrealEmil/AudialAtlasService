using AudialAtlasService.Models;
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

        public async Task<int> UserAuthentication(string apiBaseUrl, string userName, string password)
        {
            //Console.WriteLine($"{apiBaseUrl}{userName}{password}");
            //Console.ReadKey();

            //var stringUrl = $"{apiBaseUrl}/users/login/{userName}/{password}";


            HttpResponseMessage response = await httpClient.GetAsync($"{apiBaseUrl}/users/login/{userName}/{password}");

            if (response.IsSuccessStatusCode)
            {
                string authenticatedUser = await response.Content.ReadAsStringAsync();
                int authenticatedUserId = int.Parse(authenticatedUser);

                if (authenticatedUserId > 0)
                {
                    return authenticatedUserId;
                }

                //var authenticatedUser = JsonConvert.DeserializeObject<User>(content);

                //return authenticatedUser?.UserId ?? -1;
            }

            return -1; // Authentication failed
        }


        public async Task<List<SongListAllViewModel>> GetUserLikedSongsAsync(int userId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{_apiBaseUrl}/users/{userId}/songs");

            if (response.IsSuccessStatusCode )
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