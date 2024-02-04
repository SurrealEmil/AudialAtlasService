using AudialAtlasService.Services.DeezerService.Models.DTOs;
using AudialAtlasService.Services.DeezerService.Models.ViewModels;
using System.Text.Json;
using static AudialAtlasService.Services.DeezerService.Models.DTOs.DeezerTopFiveSongsDTO;

namespace AudialAtlasService.Services.DeezerService
{
    public interface IDeezerSearchFunction
    {
        Task<DeezerArtistViewModel> GetArtistViaSearchStringAsync(string artistNameQuery);
        Task<List<DeezerTopFiveSongsViewModel>> GetTopFiveSongsOfArtist(string artistNameQuery, DeezerArtistViewModel artistViewModel);
    }

    public class DeezerSearchService : IDeezerSearchFunction
    {
        private HttpClient _client;

        public DeezerSearchService() : this(new HttpClient()) { }
        public DeezerSearchService(HttpClient client) { _client = client; }


        public async Task<DeezerArtistViewModel> GetArtistViaSearchStringAsync(string artistNameQuery)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.deezer.com/search?q={artistNameQuery}")
            };

            DeezerArtistViewModel result = new DeezerArtistViewModel();

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                DeezerSearchResultDTO responseBody = JsonSerializer.Deserialize<DeezerSearchResultDTO>(body);

                result = new DeezerArtistViewModel()
                {
                    Id = responseBody.Data[0].DeezerArtistInfo.Id,
                    Name = responseBody.Data[0].DeezerArtistInfo.Name
                };
            }

            return result;
        }

        public async Task<List<DeezerTopFiveSongsViewModel>> GetTopFiveSongsOfArtist(string artistNameQuery, DeezerArtistViewModel artistViewModel)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.deezer.com/artist/{artistViewModel.Id}/top")
            };

            List<DeezerTopFiveSongsViewModel> result = new List<DeezerTopFiveSongsViewModel>();

            try
            {
                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    DeezerTopFiveSongsDTO dto = JsonSerializer.Deserialize<DeezerTopFiveSongsDTO>(body);

                    int i = 0;
                    foreach (DeezerTopFiveSongsDtoTracks song in dto.Data)
                    {
                        DeezerTopFiveSongsViewModel topFiveAddToResultList = new DeezerTopFiveSongsViewModel()
                        {
                            Id = dto.Data[i].Id,
                            Title = dto.Data[i].Title,
                        };
                        result.Add(topFiveAddToResultList);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FailedGettingTopFiveSongsForArtist_ArtistNotFound($"No artist with name {artistNameQuery} found. Error message: {ex.Message}");
            }


            return result;
        }
    }
}
