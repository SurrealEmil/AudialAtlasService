using AudialAtlasService.Models.ViewModels;
using AudialAtlasServiceClient.Services;
using Newtonsoft.Json;
using System.Net.Http;

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
