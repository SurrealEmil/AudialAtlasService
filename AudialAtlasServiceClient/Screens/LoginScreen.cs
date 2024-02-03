using AudialAtlasService.Models;
using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    public class LoginScreen : ScreenBase
    {
        public LoginScreen(IAudialAtlasApiService apiService) : base(apiService) { }
        public async Task LoginMenuAsync()
        {
            PrintLogo.PrintAudialAtlasLogo();

            string pageHeader = "\tWelcome to The Audial Atlas Service!\n" +
                                "\t______________________________________\n";
            string[] menuOptions =
            {
                        "Login",
                        "Sign up",
                        "Exit"
                    };

            int choice = MenuHandler.DisplayMainMenu(pageHeader, menuOptions);
            Console.CursorVisible = true;

            switch (choice)
            {
                case 1:
                    var userAuthenticationScreen = new UserAuthenticationScreen(ApiService);
                    await userAuthenticationScreen.CheckLoginAsync();
                    break;
                case 2:
                    await new AddUserScreen(ApiService).AddUserAsync();
                    break;
                case 3:
                    Console.WriteLine("Exiting application...");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
