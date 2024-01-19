using AudialAtlasService.Data;
using AudialAtlasService.Handlers;
using AudialAtlasService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;

namespace AudialAtlasService.Handlers
{
    // Separata Interfaces för ändamål att följa konceptet SoC (seperation of concerns).
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

    public interface IViewModelFunctions
    {
        void GetAllUsersWithViewModel();
    }

    public interface IGetUserFunctions
    {
        // Beslöt för att göra så metoderna retunerar en lista.
        List<Song> GetAllSongsLikedByUser(int userId);

        List<Artist> GetAllArtistsLikedByUser(int userId);

        List<Genre> GetAllGenresLikedByUser(int userId);
        // -------------------------------------------------

        //Based on?
        void GetRecommendations();
    }

    public interface IPostUserFunctions
    {
        //Ändra till vanlig.
        bool CheckIfUserExists(UserDTO userDTO);

        void ConnectUserToArtist(string userName, int artistId);

        void ConnectUserToSong(string userName, int songId);

        void ConnectUserToGenre(int userId, int genreId);

        void RemoveUser(string userName);
    }

    /// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

    public class UserHandler : IGetUserFunctions, IPostUserFunctions, IViewModelFunctions
    {//IoC strukturerat för att följa konceptet "lösa kopplingar".
        //Bonus: Effektiv testning och flexibilitet för moddning.

        //Sätter en fast koppling vid start, ser till att vid denna DI så går inte kopplingen att ändras av misstag.
        private readonly ApplicationContext _context;
        //---------------------------------------------


        //En konstruktor som tillåter Dependency Injection, med context kopplat i för att ge metoderna en DI koppling till databasen
        public UserHandler(ApplicationContext context)
        {
            _context = context;
        }
        //---------------------------------------------

        public bool CheckIfUserExists(UserDTO userDTO)        
        {
            return _context.Users.Any(u => u.UserName == userDTO.UserName);
        }

        public void ConnectUserToArtist(string userName, int artistId)
        {
            var user = _context.Users
                .Include(u => u.Artists)
                .FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {
                var artist = _context.Artists.Find(artistId); if (artist != null)
                {
                    if (!user.Artists.Contains(artist))
                    {
                        user.Artists.Add(artist);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //Error hitta inte artist
                }
            }
            else
            {
                // Error hitta inte user
            }
        }

        public void ConnectUserToGenre(int userId, int genreId)
        {
            var user = _context.Users
                .Include(u => u.Genres)
                .FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                var genre = _context.Genres.Find(genreId); if (genre != null)
                {
                    if (!user.Genres.Contains(genre))
                    {
                        user.Genres.Add(genre);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //Error hitta inte genre
                }
            }
            else
            {
                //Error hitta inte user
            }
        }

        public void ConnectUserToSong(string userName, int songId)
        {
            var user = _context.Users
                .Include(u => u.Songs)
                .FirstOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                var song = _context.Songs.Find(songId); if (song != null)
                {
                    if (!user.Songs.Contains(song))
                    {
                        user.Songs.Add(song);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //Error hitta inte genre.
                }
            }
            else
            {
                //Error hitta inte user.
            }
        }

        public List<Artist> GetAllArtistsLikedByUser(int userId)
        {
            var userWithLikedArtists = _context.Users
                .Include(u => u.Artists)
                .FirstOrDefault(u => u.UserId == userId);
            
            return userWithLikedArtists?.Artists.ToList() ?? new List<Artist>();
        }                                               //^^ "??" ser till att en lista skickas
                                                        //även fast det inte finns en istället för null.
        public List<Genre> GetAllGenresLikedByUser(int userId)
        {
            var userWithGenres = _context.Users
                .Include(u => u.Genres)
                .FirstOrDefault(u => u.UserId == userId);

            return userWithGenres?.Genres.ToList() ?? new List<Genre>();
        }

        public List<Song> GetAllSongsLikedByUser(int userId)
        {
            var userWithSongs = _context.Users
                .Include(u => u.Songs)
                .FirstOrDefault(u => u.UserId == userId);

            return userWithSongs?.Songs.ToList() ?? new List<Song>();
        }

        public void GetAllUsersWithViewModel()
        {
            throw new NotImplementedException();
        }

        public void GetRecommendations()
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(string userName)
        {
            
        }
    }
}
