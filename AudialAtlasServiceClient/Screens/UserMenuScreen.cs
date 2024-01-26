using AudialAtlasService.Models;
using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    internal class UserMenuScreen
    {
        public static async Task UserMenuAsync(User user, string apiBaseUrl)
        {
            string pageHeader = $"~~~~ Welcome, {user.UserName} ~~~~";
            string[] menuOptions =
            {
                "List all favorite songs",
                "List all favorite artists",
                "List all favorite genres",
                "Add new favorite song",
                "Add new favorite artist",
                "Add new favorite genre",
                "Log out"
            };

            while (true)
            {
                Console.Clear();
                int choice = MenuHandler.DisplayUserMenu(pageHeader, menuOptions);
                Console.CursorVisible = true;

                switch (choice)
                {
                    case 1:
                        await new FavoriteSongsScreen(apiBaseUrl).ListFavoriteSongsAsync();
                        break;
                    //case 2:
                    //    await FavoriteArtistsScreen.ListFavoriteArtistsAsync();
                    //    break;
                    case 7:
                        Console.WriteLine("Logging out...");
                        Thread.Sleep(1000);
                        return;
                }
            }
        }
    }
}
