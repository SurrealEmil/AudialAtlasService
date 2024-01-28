using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    public class ScreenBase
    {
        protected AudialAtlasApiService ApiService { get; }

        public ScreenBase(string apiBaseUrl)
        {
            ApiService = new AudialAtlasApiService(apiBaseUrl);
        }
    }
}
