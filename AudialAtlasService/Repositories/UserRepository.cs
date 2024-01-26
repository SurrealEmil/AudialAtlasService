using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using AudialAtlasService.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

// IoC (Interests of Concerns) strukturerat
namespace AudialAtlasService.Repositories
{
    public interface IUserRepository
    {
        bool CheckIfUserExists(string userName);
        List<ArtistDto> GetAllArtistsLikedByUser(int userId);
        List<GenreDto> GetAllGenresLikedByUser(int userId);
        List<SongDto> GetAllSongsLikedByUser(int userId);
        void ConnectUserToArtist(UserArtistConnectionDto connectionDto);
        void ConnectUserToSong(UserSongConnectionDto connectionDto);
        void ConnectUserToGenre(UserGenreConnectionDto connectionDto);
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

        public void ConnectUserToArtist(UserArtistConnectionDto connectionDto)
        {
            var user = _context.Users
                .Include(u => u.Artists)
                .FirstOrDefault(u => u.UserId == connectionDto.UserId);
            var artist = _context.Artists.Find(connectionDto.ArtistId);

            if (user == null || artist == null)
            {
                throw new KeyNotFoundException("The user or artist does not exist.");
            }

            if (user.Artists.Any(a => a.ArtistId == artist.ArtistId))
            {

                return;
            }

            user.Artists.Add(artist);
            _context.SaveChanges();
        }

        public void ConnectUserToGenre(UserGenreConnectionDto connectionDto)
        {
            var user = _context.Users
                .Include(u => u.Genres)
                .FirstOrDefault(u => u.UserId == connectionDto.UserId);
            var genre = _context.Genres.Find(connectionDto.GenreId);

            if (user == null || genre == null)
            {
                throw new KeyNotFoundException("The user or genre does not exist.");
            }

            if (user.Genres.Any(a => a.GenreId == genre.GenreId))
            {

                return;
            }

            user.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void ConnectUserToSong(UserSongConnectionDto connectionDto)
        {
            var user = _context.Users
                .Include(u => u.Songs)
                .FirstOrDefault(u => u.UserId == connectionDto.UserId);
            var song = _context.Songs.Find(connectionDto.SongId);

            if (user == null || song == null)
            {
                throw new KeyNotFoundException("The user or song does not exist.");
            }

            if (user.Songs.Any(a => a.SongId == song.SongId))
            {

                return;
            }

            user.Songs.Add(song);
            _context.SaveChanges();
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
    }
}
