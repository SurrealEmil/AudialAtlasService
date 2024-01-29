using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    public class LoginScreen
    {
        public async Task LoginMenuAsync(IAudialAtlasApiService apiService)
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
                    await UserAuthenticationScreen.CheckLogInAsync(apiService);
                    break;
                case 3:
                    Console.WriteLine("Logging out...");
                    Thread.Sleep(1000);
                    return;
            }
        }
    }
}
