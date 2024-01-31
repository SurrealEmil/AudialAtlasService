using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    public class UserMenuScreen : ScreenBase
    {
        private readonly UserAuthenticationScreen _userAuthenticationScreen;
        public UserMenuScreen(IAudialAtlasApiService apiService, UserAuthenticationScreen userAuthenticationScreen) : base(apiService)
        {
            _userAuthenticationScreen = userAuthenticationScreen;
        }
        public async Task UserMenuAsync(int userId)
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
                        await new FavoriteSongsScreen(ApiService).ListFavoriteSongsAsync(userId);
                        break;
                    case 2:
                        await new FavoriteArtistsScreen(ApiService).ListFavoriteArtistsAsync(userId);
                        break;
                    case 3:
                        await new FavoriteGenresScreen(ApiService).ListFavoriteGenresAsync(userId);
                        break;
                    case 4:
                        await new AddNewSongScreen(ApiService).AddNewFavoriteSongAsync(userId);
                        break;
                    case 7:
                        _userAuthenticationScreen.ReturnToLoginMenu();  // Set the flag to return to the login menu
                        return;
                }
            }
        }
    }
}
