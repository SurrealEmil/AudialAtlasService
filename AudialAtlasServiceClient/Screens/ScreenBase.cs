using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    public class ScreenBase
    {
        protected IAudialAtlasApiService ApiService { get; }

        public ScreenBase(IAudialAtlasApiService apiService)
        {
            ApiService = apiService;
        }
    }
}
