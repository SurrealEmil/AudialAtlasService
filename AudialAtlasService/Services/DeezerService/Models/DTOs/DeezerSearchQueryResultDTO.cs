using System.Text.Json.Serialization;

namespace AudialAtlasService.Services.DeezerService.Models.DTOs
{
    public class DeezerSearchResultDTO
    {
        [JsonPropertyName("data")]
        public List<DeezerSearchResultData> Data { get; set; }

        public class DeezerSearchResultData
        {
            [JsonPropertyName("artist")]
            public DeezerSearchResultArtist DeezerArtistInfo { get; set; }
        }

        public class DeezerSearchResultArtist
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
    }

    
}