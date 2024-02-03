using AudialAtlasServiceClient.Models.DTOs.AddDTO;
using AudialAtlasServiceClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceClient.Screens.Add
{
    internal class AddUserScreen : ScreenBase
    {
        public AddUserScreen(IAudialAtlasApiService apiService) : base(apiService) { }

        public async Task AddUserAsync()
        {
            Console.WriteLine("~~~~ Audial Atlas Client - Add new user ~~~~");

            Console.Write("\nPlease enter username: ");
            string username = Console.ReadLine();

            Console.Write("\nPlease enter firstname: ");
            string firstname = Console.ReadLine();

            Console.Write("\nPlease enter lastname: ");
            string lastname = Console.ReadLine();

            Console.Write("Please enter password: ");
            string password = Console.ReadLine();

            try
            {
                AddUserDto addUserDto = new AddUserDto
                {
                    UserName = username,
                    FirstName = firstname,
                    LastName = lastname,
                    Password = password
                };

                await ApiService.PostNewUserAsync(addUserDto);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding new user: {ex.Message}");
            }
        }
    }
}
