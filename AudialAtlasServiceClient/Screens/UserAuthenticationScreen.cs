using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Repositories;
using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;
using System.Text;

namespace AudialAtlasServiceClient.Screens
{
    public class UserAuthenticationScreen : ScreenBase
    {
        public UserAuthenticationScreen(string apiBaseUrl) : base(apiBaseUrl) { }

        public async Task CheckLogInAsync(string apiBaseUrl)
        {
            Console.WriteLine("\n\tAudio Atlas login - Please provide your login credentials:");

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
            string? password = Console.ReadLine();

            if (password != null)
            {
            }
            else
            {
                // Invalid input or null password
                Console.WriteLine("Invalid input. Please try again.");
                Console.ReadLine();
                return;
            }

            var userId = await ApiService.UserAuthentication(apiBaseUrl, userName, password);

            if (userId != -1)
            {
                await UserMenuScreen.UserMenuAsync(userId, apiBaseUrl);
            }
            else
            {
                Console.WriteLine("\nWrong username or password. Please try again.");
                Console.ReadKey();
            }
        }

        //        if (CheckPassword(user))
        //    {
        //        await UserMenuScreen.UserMenuAsync(user, apiBaseUrl);
        //    }
        //    else
        //    {
        //        Console.WriteLine("\n\n\tToo many failed attempts. Locking user...");
        //        Thread.Sleep(1000);
        //        Cooldown();
        //    }
        //}

        //private static bool CheckPassword(User user)
        //{
        //    const int maxLoginAttempts = 3;

        //    Console.WriteLine($"\n\tPlease enter your password.");

        //    for (int i = 1; i <= maxLoginAttempts; i++)
        //    {
        //        Console.Write("\n\tPassword: ");

        //        string enteredPassword = HidePin();

        //        if (user != null && enteredPassword == user.Password)
        //        {
        //            Console.WriteLine($"\n\n\tPassword correct. \n\n\tWelcome {user.UserName}!\n\n\tLogging in...");
        //            Thread.Sleep(1000);
        //            return true;
        //        }
        //        else if (i < 2)
        //        {
        //            Console.WriteLine($"\n\n\tInvalid password. Please try again. {maxLoginAttempts - i} tries left.");
        //        }
        //        if (i == 2)
        //        {
        //            Console.WriteLine($"\n\n\tInvalid password. Please try again. 1 try left.");
        //        }
        //    }
        //    return false;
        //}

        //// Replace user PIN input with '*'
        //public static string HidePin()
        //{
        //    StringBuilder pin = new StringBuilder();

        //    while (true)
        //    {
        //        // Use 'true' to hide which key is being pressed
        //        ConsoleKeyInfo key = Console.ReadKey(true);

        //        // Check if user press Backspace or Enter
        //        if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
        //        {
        //            pin.Append(key.KeyChar);
        //            Console.Write('*');
        //        }
        //        else if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
        //        {
        //            // Delete the last user input
        //            pin.Length -= 1;

        //            /* Visual representation of the deletion:
        //               Move cursor back one position, 
        //               overwrite '*' with a space character,
        //               move the cursor back one position.*/
        //            Console.Write("\b \b");
        //        }

        //        if (key.Key == ConsoleKey.Enter)
        //        {
        //            break;
        //        }
        //    }
        //    return pin.ToString();
        //}

        //// Lock user after three failed password attempts
        //private static bool Cooldown()
        //{
        //    const int jailTime = 180; // 3 min

        //    Console.Clear();
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    PrintLogo.PrintLockout();
        //    Console.WriteLine("\tYou ran out of tries. Lockout timer initiated.");
        //    Console.CursorVisible = false;
        //    for (int cooldown = jailTime; cooldown >= 0; cooldown--)
        //    {
        //        Console.SetCursorPosition(0, 13);
        //        int minutes = cooldown / 60;
        //        int remainingcooldown = cooldown % 60;
        //        Console.WriteLine($"\n\tTry again in: {minutes:D2}m {remainingcooldown:D2}s");
        //        Thread.Sleep(1000); // Sleep for 1 second to refresh timer properly
        //    }
        //    Console.CursorVisible = true;
        //    Console.Clear();
        //    Console.WriteLine("\tLockout is over. Press Enter to go back to the start screen:");
        //    Console.ReadLine();

        //    return true;
        //}
    }
}

