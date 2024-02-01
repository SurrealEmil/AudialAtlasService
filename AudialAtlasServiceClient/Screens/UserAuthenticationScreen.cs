using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;
using System.Text;

namespace AudialAtlasServiceClient.Screens
{
    public class UserAuthenticationScreen : ScreenBase 
    {
        private bool returnToLoginMenu; // Flag to determine whether to return to the login menu
        public UserAuthenticationScreen(IAudialAtlasApiService service) : base(service) { }
        public async Task CheckLoginAsync()
        {
            returnToLoginMenu = false; // Initialize the flag

            Console.WriteLine("\n\tAudial Atlas client login - Please provide your login credentials:");            

            const int maxLoginAttempts = 3;

            for (int i = 1; i <= maxLoginAttempts; i++)
            {

                if (returnToLoginMenu)  // Check the flag and return to the login menu
                {
                    break;
                }

                Console.Write("\n\tUsername: ");
                string? userName = Console.ReadLine();

                if (userName != null)
                {
                    userName = userName.Trim().ToLower();
                }
                else
                {
                    // Invalid input or null password
                    Console.WriteLine("Invalid input. Please try again.");
                }

                Console.Write("\n\tPassword: ");
                string? password = HidePassword();

                var userId = await ApiService.UserAuthentication(userName, password);

                if (userId != -1)
                {
                    var userMenuScreen = new UserMenuScreen(ApiService, this);
                    await userMenuScreen.UserMenuAsync(userId);
                }
                else if (i == 1)
                {
                    Console.WriteLine($"\n\n\tWrong username or password. Please try again. {maxLoginAttempts - i} tries left.");
                }
                else if (i == 2)
                {
                    Console.WriteLine($"\n\n\tInvalid username or password. Please try again. 1 try left.");
                }
                if (i == 3)
                {
                    Console.WriteLine("\n\n\tToo many failed attempts. Locking user...");
                    Thread.Sleep(2000);
                    Cooldown();
                }
            }
        }

        // Method to set the flag and return to the login menu
        public void ReturnToLoginMenu()
        {
            returnToLoginMenu = true;
        }


        // Replace user PIN input with '*'
        private static string HidePassword()
        {
            StringBuilder password = new StringBuilder();

            while (true)
            {
                // Use 'true' to hide which key is being pressed
                ConsoleKeyInfo key = Console.ReadKey(true);

                // Check if user press Backspace or Enter
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password.Append(key.KeyChar);
                    Console.Write('*');
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // Delete the last user input
                    password.Length -= 1;

                    /* Visual representation of the deletion:
                       Move cursor back one position, 
                       overwrite '*' with a space character,
                       move the cursor back one position.*/
                    Console.Write("\b \b");
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            return password.ToString();
        }

        // Lock user after three failed password attempts
        private static bool Cooldown()
        {
            const int jailTime = 180; // 3 min

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            PrintLogo.PrintLockout();
            Console.WriteLine("\tYou ran out of tries. Lockout timer initiated.");
            Console.CursorVisible = false;
            for (int cooldown = jailTime; cooldown >= 0; cooldown--)
            {
                Console.SetCursorPosition(0, 13);
                int minutes = cooldown / 60;
                int remainingcooldown = cooldown % 60;
                Console.WriteLine($"\n\tTry again in: {minutes:D2}m {remainingcooldown:D2}s");
                Thread.Sleep(1000); // Sleep for 1 second to refresh timer properly
            }
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine("\tLockout is over. Press Enter to go back to the start screen:");
            Console.ReadLine();

            return true;
        }
    }
}


