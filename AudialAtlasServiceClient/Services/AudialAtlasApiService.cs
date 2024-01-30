﻿using AudialAtlasServiceClient.Models.DTOs;
using AudialAtlasServiceClient.Models.ViewModels;
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

    }
}
#endregion