using AudialAtlasService.Data;
using AudialAtlasService.Repositories;
using AudialAtlasServiceClient.Handlers;

namespace AudialAtlasServiceClient.Screens
{
    public class LoginScreen
    {
        public async Task LoginMenuAsync(string apiBaseUrl)
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

            var userAuthenticationScreen = new UserAuthenticationScreen(apiBaseUrl);

            switch (choice)
            {
                case 1:
                    await userAuthenticationScreen.CheckLogInAsync(apiBaseUrl);
                    break;
                case 3:
                    Console.WriteLine("Logging out...");
                    Thread.Sleep(1000);
                    return;
            }
        }
    }
}
