using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;
using AudialAtlasServiceClient.Screens;

namespace AudialAtlasServiceClient.Screens
{
    internal static class UserMenuScreen
    {
        public static async Task UserMenuAsync(IAudialAtlasApiService apiService, int userId)
        {
            string pageHeader = "~~~~ Audial Atlas Client - Main menu ~~~~";
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
                        await new FavoriteSongsScreen(apiService).ListFavoriteSongsAsync(userId);
                        break;
                    case 2:
                        await new FavoriteArtistsScreen(apiService).ListFavoriteArtistsAsync(userId);
                        break;
                    case 3:
                        await new FavoriteGenresScreen(apiService).ListFavoriteGenresAsync(userId);
                        break;
                    case 4:
                        await new AddNewSongScreen(apiService).AddNewFavoriteSongAsync(userId);
                        break;
                    case 7:
                        Console.WriteLine("Logging out...");
                        Thread.Sleep(1000);
                        return;
                }
            }
        }
    }
}
