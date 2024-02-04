using AudialAtlasService.Services.DeezerService.Models.ViewModels;

namespace AudialAtlasService.Services.DeezerService.Handler
{
    public class DeezerServiceHandler
    {
        public static async Task<IResult> GetTopFiveSongsOfArtist(IDeezerSearchFunction service, string artistNameQuery)
        {
            try
            {
                DeezerArtistViewModel firstGet = await service.GetArtistViaSearchStringAsync(artistNameQuery);

                List<DeezerTopFiveSongsViewModel> result = await service.GetTopFiveSongsOfArtist(artistNameQuery, firstGet);

                return Results.Json(result);
            }
            catch (FailedGettingTopFiveSongsForArtist_ArtistNotFound ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

    }
}
