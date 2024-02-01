using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using AudialAtlasService.Models.ViewModels.UserViewModels;
using Microsoft.EntityFrameworkCore;

namespace AudialAtlasService.Repositories
{
    public interface IUserRepository
    {
        bool CheckIfUserExists(string userName);
        List<GetAllUsersViewModel> GetAllUsers();
        List<ArtistListAllFromUserViewModel> GetAllArtistsLikedByUser(int userId);
        List<GenreListAllFromUserViewModel> GetAllGenresLikedByUser(int userId);
        List<SongSingleViewModel> GetAllSongsLikedByUser(int userId);
        int AuthenticateUser(string userName, string password);
        void ConnectUserToArtist(UserArtistConnectionDto connectionDto);
        void ConnectUserToSong(UserSongConnectionDto connectionDto);
        void ConnectUserToGenre(UserGenreConnectionDto connectionDto);
        void GetRecommendations();
        void AddUser(UserDto dto);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
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

        public int AuthenticateUser(string userName, string password)
        {
            var authenticatedUserId = _context.Users
                 .SingleOrDefault(u => u.UserName == userName && u.Password == password);

            return authenticatedUserId?.UserId ?? -1;
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

        public List<ArtistListAllFromUserViewModel> GetAllArtistsLikedByUser(int userId)
        {
            Console.WriteLine("\nBefore digging in Db.\n");

            var artist = _context.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Artists)
                .Select(h => new ArtistListAllFromUserViewModel
                {
                    Name = h.Name,
                    Description = h.Description

                })
                .ToList();

            Console.WriteLine("\nDigging done, applying result.\n");

            return artist;
        }

        public List<GenreListAllFromUserViewModel> GetAllGenresLikedByUser(int userId)
        {
            Console.WriteLine("\nPre catch\n");

            var genres = _context.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Genres)
                .Select(h => new GenreListAllFromUserViewModel
                {
                    GenreTitle = h.GenreTitle
                })
                .ToList();

            Console.WriteLine("\nPost catch\n");

            return genres;
        }

        public List<SongSingleViewModel> GetAllSongsLikedByUser(int userId)
        {
            Console.WriteLine("\nBefore digging in Db.\n");

            var songs = _context.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Songs)
                .Select(h => new SongSingleViewModel
                {
                    SongTitle = h.SongTitle,
                    Artist = h.Artist.Name,
                    Genres = h.Genres
                        .Select(g => g.GenreTitle)
                        .ToArray()
                })
                .ToList();

            Console.WriteLine("\nDigging done, applying result.\n");

            return songs;
        }

        public void GetRecommendations()
        {
            throw new NotImplementedException();
        }

        public void AddUser(UserDto dto)
        {
            try
            {
                User user = new User()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    UserName = dto.UserName,
                    Password = dto.Password
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public List<GetAllUsersViewModel> GetAllUsers()
        {
            var userList = _context.Users
                .Select(h => new GetAllUsersViewModel
                {
                    UserName = h.UserName,
                })
                .ToList();

            return userList;
        }
    }
}
