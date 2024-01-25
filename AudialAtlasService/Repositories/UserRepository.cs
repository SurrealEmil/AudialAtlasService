using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using AudialAtlasService.Handlers;
using Microsoft.AspNetCore.Mvc;

// IoC (Interests of Concerns) strukturerat
namespace AudialAtlasService.Repositories
{
    public interface IUserRepository
    {
        bool CheckIfUserExists(string userName);
        List<ArtistDto> GetAllArtistsLikedByUser(int userId);
        List<GenreDto> GetAllGenresLikedByUser(int userId);
        List<SongDto> GetAllSongsLikedByUser(int userId);
        void ConnectUserToArtist(string userName, int artistId);
        void ConnectUserToSong(string userName, int songId);
        void ConnectUserToGenre(int userId, int genreId);
        void RemoveUser(string userName);
        void GetRecommendations();
    }

    public class UserRepository : IUserRepository
    {
        //Sätter en fast koppling vid start.
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {//En konstruktor som tillåter Dependency Injection.
            _context = context;
        }

        public bool CheckIfUserExists(string userName)
        {
            Console.WriteLine("Pre user exists");
            var userExists = _context.Users.
                Where(x => x.UserName == userName).
                Any(x => x.UserName == userName);
            Console.WriteLine("Post user exists");
            return userExists;
        }

        public void ConnectUserToArtist(string userName, int artistId)
        {
            throw new NotImplementedException();
        }

        public void ConnectUserToGenre(int userId, int genreId)
        {
            throw new NotImplementedException();
        }

        public void ConnectUserToSong(string userName, int songId)
        {
            throw new NotImplementedException();
        }

        public List<ArtistDto> GetAllArtistsLikedByUser(int userId)
        {
            Console.WriteLine("\nBefore digging in Db.\n");

            var artist = _context.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Artists)
                .Select(h => new ArtistDto
                {
                    Name = h.Name,
                    Description = h.Description

                })
                .ToList();

            Console.WriteLine("\nDigging done, applying result.\n");

            return artist;
        }

        public List<GenreDto> GetAllGenresLikedByUser(int userId)
        {
            Console.WriteLine("\nPre catch\n");

            var genres = _context.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Genres)
                .Select(h => new GenreDto
                {
                    // GenreId = h.GenreId,
                    GenreTitle = h.GenreTitle
                })
                .ToList();

            Console.WriteLine("\nPost catch\n");

            return genres;
        }

        public List<SongDto> GetAllSongsLikedByUser(int userId)
        {
            Console.WriteLine("\nBefore digging in Db.\n");

            var song = _context.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Songs)
                .Select(h => new SongDto
                {
                    SongTitle = h.SongTitle
                })
                .ToList();

            Console.WriteLine("\nDigging done, applying result.\n");

            return song;
        }

        public void GetRecommendations()
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(string userName)
        {
            var user = _context.Users.Find(userName);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Could not find user");
            }

        }
    }
}
