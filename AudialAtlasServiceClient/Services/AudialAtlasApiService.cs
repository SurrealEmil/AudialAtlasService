using AudialAtlasService.Models;
using AudialAtlasServiceClient.Models.DTOs.AddDTO;
using AudialAtlasServiceClient.Models.DTOs.FavoritesDTO;
using AudialAtlasServiceClient.Models.ViewModels.FavoriteView;
using AudialAtlasServiceClient.Models.ViewModels.ListAllView;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AudialAtlasServiceClient.Services
{
    public interface IAudialAtlasApiService
    {
        Task<int> UserAuthentication(string username, string password);
        Task<List<FavoriteSongViewModel>> GetUserFavoriteSongsAsync(int userId);
        Task<List<FavoriteArtistViewModel>> GetUserFavoriteArtistsAsync(int userId);
        Task<List<FavoriteGenreViewModel>> GetUserFavoriteGenresAsync(int userId);
        Task PostNewFavoriteSongAsync(int userId, AddFavoriteSongDto addFavoriteSongDto);
        Task PostNewUserAsync(AddUserDto addUserDto);
        Task PostNewGenreAsync(AddGenreDto addGenreDto);
        Task PostNewArtistAsync(AddArtistDto addArtistDto);
        Task PostNewSongAsync(int artistId, int genreId, AddSongDto addSongDto);
        Task<List<ListAllSongsInDbViewModel>> GetAllSongsFromDbAsync();
        Task<List<ListAllArtistsInDbViewModel>> GetAllArtistsFromDbAsync();
        Task<List<ListAllGenresInDbViewModel>> GetAllGenresFromDbAsync();
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

        public async Task<int> UserAuthentication(string userName, string password)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/users/login/{userName}/{password}");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string authenticatedUser = await response.Content.ReadAsStringAsync();
                    int authenticatedUserId = int.Parse(authenticatedUser);

                    if (authenticatedUserId > 0)
                    {
                        return authenticatedUserId;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to authenticate user. Error: {ex.Message}");
            }
            return -1; // Authentication failed
        }

        public async Task<List<FavoriteSongViewModel>> GetUserFavoriteSongsAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/users/{userId}/songs");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    var songList = JsonConvert.DeserializeObject<List<FavoriteSongViewModel>>(result);

                    return songList ?? new List<FavoriteSongViewModel>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<FavoriteSongViewModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get favorite songs. Error: {ex.Message}");
            }
            return new List<FavoriteSongViewModel>();
        }

        public async Task<List<FavoriteArtistViewModel>> GetUserFavoriteArtistsAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/users/{userId}/artists");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    var artistList = JsonConvert.DeserializeObject<List<FavoriteArtistViewModel>>(result);

                    return artistList ?? new List<FavoriteArtistViewModel>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<FavoriteArtistViewModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get favorite artists. Error: {ex.Message}");
            }
            return new List<FavoriteArtistViewModel>();
        }

        public async Task<List<FavoriteGenreViewModel>> GetUserFavoriteGenresAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/users/{userId}/genres");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    var genreList = JsonConvert.DeserializeObject<List<FavoriteGenreViewModel>>(result);

                    return genreList ?? new List<FavoriteGenreViewModel>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<FavoriteGenreViewModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get favorite genres. Error: {ex.Message}");
            }
            return new List<FavoriteGenreViewModel>();
        }

        public async Task PostNewFavoriteSongAsync(int userId, AddFavoriteSongDto addFavoriteSongDto)
        {
            try
            {
                // Serialize the AddFavoriteSongDto object to JSON
                string jsonContent = JsonConvert.SerializeObject(addFavoriteSongDto);

                // Create the HttpContent for the request
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiBaseUrl}/users/connect-to-song", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Song added successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to add the song. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add the song. Error: {ex.Message}");
            }
        }

        public async Task PostNewUserAsync(AddUserDto addUserDto)
        {
            try
            {
                // Serialize the AddUserDto object to JSON
                string jsonContent = JsonConvert.SerializeObject(addUserDto);

                // Create the HttpContent for the request
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiBaseUrl}/users", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User added successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to add the user. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add the user. Error: {ex.Message}");
            }
        }

        public async Task PostNewGenreAsync(AddGenreDto addGenreDto)
        {
            try
            {
                // Serialize the AddUserDto object to JSON
                string jsonContent = JsonConvert.SerializeObject(addGenreDto);

                // Create the HttpContent for the request
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiBaseUrl}/genres", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Genre added successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to add the genre. Status code: {response.StatusCode}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add the genre. Error: {ex.Message}");
                Console.ReadLine();
            }
        }

        public async Task PostNewArtistAsync(AddArtistDto addArtistDto)
        {
            try
            {
                // Serialize the AddUserDto object to JSON
                string jsonContent = JsonConvert.SerializeObject(addArtistDto);

                // Create the HttpContent for the request
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiBaseUrl}/artists", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Artist added successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to add the artist. Status code: {response.StatusCode}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add the artist. Error: {ex.Message}");
                Console.ReadLine();
            }
        }

        public async Task PostNewSongAsync(int artistId, int genreId, AddSongDto addSongDto)
        {
            try
            {
                // Serialize the AddSongDto object to JSON
                string jsonContent = JsonConvert.SerializeObject(addSongDto);

                // Create the HttpContent for the request
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiBaseUrl}/artists/{artistId}/genres/{genreId}/songs", content);

                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Song added successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to add the song. Status code: {response.StatusCode}");
                    Console.WriteLine($"Response content: {responseContent}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add the song. Error: {ex.Message}");
                Console.ReadLine();
            }
        }

        public async Task<List<ListAllSongsInDbViewModel>> GetAllSongsFromDbAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/songs");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    List<ListAllSongsInDbViewModel>? songList = JsonConvert.DeserializeObject<List<ListAllSongsInDbViewModel>>(result);

                    return songList;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<ListAllSongsInDbViewModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get songs. Error: {ex.Message}");
            }
            return new List<ListAllSongsInDbViewModel>();
        }

        public async Task<List<ListAllArtistsInDbViewModel>> GetAllArtistsFromDbAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/artists");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    List<ListAllArtistsInDbViewModel>? artistList = JsonConvert.DeserializeObject<List<ListAllArtistsInDbViewModel>>(result);

                    return artistList;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<ListAllArtistsInDbViewModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get artists. Error: {ex.Message}");
            }
            return new List<ListAllArtistsInDbViewModel>();
        }

        public async Task<List<ListAllGenresInDbViewModel>> GetAllGenresFromDbAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/genres");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    List<ListAllGenresInDbViewModel>? genresList = JsonConvert.DeserializeObject<List<ListAllGenresInDbViewModel>>(result);

                    return genresList;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<ListAllGenresInDbViewModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get genres. Error: {ex.Message}");
            }
            return new List<ListAllGenresInDbViewModel>();
        }
    }
}