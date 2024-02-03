using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Services;

namespace AudialAtlasServiceClient.Screens
{
    public class UserMenuScreen : ScreenBase
    {
        private readonly UserAuthenticationScreen userAuthenticationScreen;
        public UserMenuScreen(IAudialAtlasApiService apiService, UserAuthenticationScreen userAuthenticationScreen) : base(apiService)
        {
            this.userAuthenticationScreen = userAuthenticationScreen;
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
                "All songs",
                "All artists",
                "All genres",
                "Add songs",
                "Add artists",
                "Add genres",
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
                        var favoriteSongsScreen = new FavoriteSongsScreen(ApiService);
                        await favoriteSongsScreen.ListFavoriteSongsAsync(userId);
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
                        await new ListAllSongsInDbScreen(ApiService).ListAllSongsInDbAsync();
                        break;
                    case 8:
                        await new ListAllArtistsInDbScreen(ApiService).ListAllArtistsInDbAsync();
                        break;
                    case 9:
                        await new ListAllGenresInDbScreen(ApiService).ListAllGenresInDbAsync();
                        break;
                    //case 10:
                    //    await new ListAllGenresInDbScreen(ApiService).ListAllGenresInDbAsync();
                    //    break;
                    //case 11:
                    //    await new ListAllGenresInDbScreen(ApiService).ListAllGenresInDbAsync();
                    //    break;
                    case 12:
                        await new AddGenreScreen(ApiService).AddGenreAsync();
                        break;
                    case 13:
                        userAuthenticationScreen.ReturnToLoginMenu();  // Set the flag to return to the login menu
                        return;
                        
                }
            }
        }
    }
}
