using AudialAtlasServiceClient.Screens;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient
{
    class AudialAtlasClientApplication
    {
        private readonly LoginScreen _loginScreen;

        public AudialAtlasClientApplication(LoginScreen loginScreen)
        {
            _loginScreen = loginScreen;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                await _loginScreen.LoginMenuAsync();
            }
        }
    }
}
