using AudialAtlasServiceClient.Handlers;
using AudialAtlasServiceClient.Screens.Add;
using AudialAtlasServiceClient.Screens.ConnectFavorites;
using AudialAtlasServiceClient.Screens.ListAll;
using AudialAtlasServiceClient.Screens.ListFavorites;
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
                "All songs",
                "All artists",
                "All genres",
                "Add songs",
                "Add artists",
                "Add genres",
                "Top five songs of artist",
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
                    case 5:
                        await new AddNewArtistScreen(ApiService).AddNewFavoriteArtistAsync(userId);
                        break;
                    case 6:
                        await new AddNewGenreScreen(ApiService).AddNewFavoriteGenreAsync(userId);
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
                    case 10:
                        await new AddSongScreen(ApiService).AddSongAsync();
                        break;
                    case 11:
                        await new AddArtistScreen(ApiService).AddArtistAsync();
                        break;
                    case 12:
                        await new AddGenreScreen(ApiService).AddGenreAsync();
                        break;
                    case 13:
                        await new GetTopFiveSongsOfArtistScreen(ApiService).ListFavoriteSongsAsync();
                        break;

                    case 14:
                        _userAuthenticationScreen.ReturnToLoginMenu();  // Set the flag to return to the login menu
                        return;

                }
            }
        }
    }
}
