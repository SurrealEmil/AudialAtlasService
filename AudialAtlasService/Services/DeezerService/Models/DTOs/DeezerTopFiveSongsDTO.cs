using System.Text.Json.Serialization;

namespace AudialAtlasService.Services.DeezerService.Models.DTOs
{
    public class DeezerTopFiveSongsDTO
    {
        [JsonPropertyName("data")]
        public List<DeezerTopFiveSongsDtoTracks> Data { get; set; }

        public class DeezerTopFiveSongsDtoTracks
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("title")]
            public string Title { get; set; }
        }
    }
}
