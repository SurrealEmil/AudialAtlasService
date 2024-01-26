//using AudialAtlasService.Models.ViewModels;
//using Newtonsoft.Json;

//namespace AudialAtlasServiceClient.Screens
//{
//    internal class FavoriteArtistsScreen
//    {
//        public static async Task ListFavoriteArtistsAsync(string apiBaseUrl, HttpClient httpClient)
//        {
//            try
//            {
//                HttpResponseMessage response = await httpClient.GetAsync($"{apiBaseUrl}/artists");

//                if (response.IsSuccessStatusCode)
//                {
//                    string result = await response.Content.ReadAsStringAsync();

//                    var artistList = JsonConvert.DeserializeObject<List<ArtistListAllViewModel>>(result);

//                    if (artistList.Any())
//                    {
//                        foreach (var artist in artistList)
//                        {
//                            if (artist != null)
//                            {
//                                Console.WriteLine($"Artist: {artist.ArtistName}");
//                                Console.Write($"Genre(s): ");

//                                if (!string.IsNullOrWhiteSpace(artist.GenreTitle))
//                                {
//                                    foreach (var genre in artist.GenreTitle)
//                                    {
//                                        Console.Write($"{genre}");
//                                    }
//                                }
//                                else
//                                {
//                                    Console.Write("No genres available.");
//                                }
//                                Console.WriteLine(); // Add spacing before the next artist
//                            }
//                            else
//                            {
//                                Console.WriteLine("No favorite artists found.");
//                            }
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("No artists found.");
//                    }
//                    // Wait for user to press ENTER key
//                    Console.WriteLine("\nPress ENTER to return to the main menu.");
//                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
//                    Console.WriteLine("\nReturning to main menu...");
//                    Thread.Sleep(1000);
//                }
//            }
//            catch (HttpRequestException ex)
//            {
//                Console.WriteLine($"Error displaying favorite songs: {ex.Message}");
//            }
//        }
//    }
//}
