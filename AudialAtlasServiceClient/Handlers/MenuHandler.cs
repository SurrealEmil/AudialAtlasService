using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudialAtlasService.Data;
using AudialAtlasService.Models;
using Microsoft.AspNetCore.Authentication;

namespace AudialAtlasServiceClient.Handlers
{
    internal class MenuHandler
    {
        //      Mayby implement ADMIN later

        //public static void DisplayMenu(BankContext context, User user)
        //{
        //    if (user.Name == "admin")
        //    {
        //        AdminFunctions.AdminMenu();
        //    }
        //    else
        //    {
        //        User.UserMenu(context, user);
        //    }
        //}

        public static int DisplayMainMenu(string pageHeader, string[] menuoptions)
        {
            // Set the cursor visibility to false for a cleaner user interface.
            Console.CursorVisible = false;

            // Initialize the selected menu option index
            int menuHeight = menuoptions.Length;
            int y = 0;

            Console.WriteLine(pageHeader);

            // Main loop for handling user input and menu navigation
            // Loop is versatile and can work with many different length menus
            while (true)
            {
                // Print the menu options with the arrow indicating the selected option.
                for (int i = 0; i < menuoptions.Length; i++)
                {
                    Console.SetCursorPosition(6, 15 + i + 2);
                    Console.Write(i == y ? "→ " : "  ");
                    Console.WriteLine(menuoptions[i]);
                }
                if (Console.KeyAvailable)
                {
                    // Takes input from user
                    var command = Console.ReadKey().Key;
                    if (command == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        return y + 1;
                    }
                    // Switch that incrementes or decrements the y axis of the arrow.
                    switch (command)
                    {
                        case ConsoleKey.UpArrow:
                            if (y > 0)
                            {
                                y--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (y < menuHeight - 1)
                            {
                                y++;
                            }
                            break;
                    }
                }
                // If no input is detected within 100 milliseconds, the loop starts over.
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        public static int DisplayUserMenu(string pageHeader, string[] menuoptions)
        {
            // Set the cursor visibility to false for a cleaner user interface.
            Console.CursorVisible = false;

            // Initialize the selected menu option index
            int menuHeight = menuoptions.Length;
            int y = 0;

            Console.WriteLine(pageHeader);

            // Main loop for handling user input and menu navigation
            // Loop is versatile and can work with many different length menus
            while (true)
            {
                // Print the menu options with the arrow indicating the selected option.
                for (int i = 0; i < menuoptions.Length; i++)
                {
                    Console.SetCursorPosition(0, +i + 2);
                    Console.Write(i == y ? "→ " : "  ");
                    Console.WriteLine(menuoptions[i]);
                }
                if (Console.KeyAvailable)
                {
                    // Takes input from user
                    var command = Console.ReadKey().Key;
                    if (command == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        return y + 1;
                    }
                    // Switch that incrementes or decrements the y axis of the arrow.
                    switch (command)
                    {
                        case ConsoleKey.UpArrow:
                            if (y > 0)
                            {
                                y--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (y < menuHeight - 1)
                            {
                                y++;
                            }
                            break;
                    }
                }
                // If no input is detected within 100 milliseconds, the loop starts over.
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
