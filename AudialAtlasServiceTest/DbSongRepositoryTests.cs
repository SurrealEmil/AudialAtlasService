using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels;
using AudialAtlasService.Models.ViewModels.GenreViewModels;
using AudialAtlasService.Repositories;
using AudialAtlasService.Repositories.SongRepoExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceTest
{
    [TestClass]
    public class DbSongRepositoryTests
    {
        [TestMethod]
        public void PostSong_CreateSong()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("PostSong_CreateSong")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                Artist artist = new Artist()
                {
                    Name = "TestArtistName",
                    Description = "TestArtistDescription"
                };
                context.Artists.Add(artist);
                context.SaveChanges();
                artist = context.Artists.Where(a => a.Name == "TestArtistName").SingleOrDefault();

                ISongRepository repository = new SongRepository(context);

                // Act
                repository.PostSong(artist.ArtistId, new SongDto
                {
                    SongTitle = "TestSongTitle"
                });

                // Assert
                List<Song> songs = context.Songs.ToList();
                Assert.AreEqual(1, songs.Count);
                Assert.AreEqual(1, context.Artists.Count());
                Assert.AreEqual(songs[0].SongTitle, "TestSongTitle");
                Assert.AreEqual(songs[0].Artist.Name, "TestArtistName");
                Assert.AreEqual(songs[0].Artist.Description, "TestArtistDescription");
            }
        }

        // Exception test
        [TestMethod]
        public void PostSong_ThrowsCorrectExceptionWhenArtistIdIsIncorrect() 
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("PostSong_ThrowsCorrectException")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {                
                ISongRepository repository = new SongRepository(context);

                // Act & Assert - StackOverFlow and ChatGPT suggested using "Assert.ThrowsException" when the [ExpectedException] was
                // failing to correctly catch the exception. Before, the program would stop in the PostSong method when the exception
                // was thrown, but only when selecting "Run all tests". Running the test individually worked. The current version
                // works everytime though.
                // Update 30 minutes later - This tests fails when running with the debugger, unless choosing continue when the test stops.
                // Make sure to press "continue" if this test stops when running tests with the debugger. The issue seems to have been that
                // I was running the tests with the debugger and then stopping (meaning failed test).
                Assert.ThrowsException<SongFailedToAddToDatabaseException>(() =>
                {
                    repository.PostSong(-1, new SongDto
                    {
                        SongTitle = "TestSongTitle"
                    });
                });
            }
        }

        [TestMethod]
        public void GetSingleSong_GetsCorrectSong()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetSingleSong_GetsCorrectSong")
                .Options;

            SongSingleViewModel song = new SongSingleViewModel();

            using (ApplicationContext context = new ApplicationContext(options))
            {
                Artist artist = new Artist()
                {
                    Name = "TestArtistName",
                    Description = "TestArtistDescription"
                };
                context.Artists.Add(artist);
                context.SaveChanges();
                artist = context.Artists.Where(a => a.Name == "TestArtistName").SingleOrDefault();

                ISongRepository repository = new SongRepository(context);

                repository.PostSong(artist.ArtistId, new SongDto
                {
                    SongTitle = "TestSongTitle"
                });

                // Act
                song = repository.GetSingleSong(1);
            }

            // Assert
            Assert.AreEqual("TestSongTitle", song.SongTitle);
        }

        [TestMethod]
        public void GetSingleSong_ThrowsCorrectExceptionWhenSongIsNull()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase("GetSingleSong_ThrowsCorrectExceptionWhenSongIsNull")
               .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                ISongRepository repository = new SongRepository(context);

                // Act & Assert
                Assert.ThrowsException<SongNotFoundException>(() => { SongSingleViewModel song = repository.GetSingleSong(0); });                
            }            
        }

        [TestMethod]
        public void ListAllSongs_ReturnsListWithCorrectSongs()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ListAllSongs_ReturnsListWithCorrectSongs")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                Artist artist = new Artist()
                {
                    Name = "TestArtistName",
                    Description = "TestArtistDescription"
                };
                context.Artists.Add(artist);
                context.SaveChanges();
                artist = context.Artists.Where(a => a.Name == "TestArtistName").SingleOrDefault();

                ISongRepository repository = new SongRepository(context);

                repository.PostSong(artist.ArtistId, new SongDto
                {
                    SongTitle = "TestSongTitle"
                });
                repository.PostSong(artist.ArtistId, new SongDto
                {
                    SongTitle = "TestSongTitle2"
                });
                repository.PostSong(artist.ArtistId, new SongDto
                {
                    SongTitle = "TestSongTitle3"
                });

                // Act
                List<SongListAllViewModel> songList = repository.ListAllSongs();

                // Assert
                Assert.AreEqual(1, songList[0].Id);
                Assert.AreEqual("TestSongTitle", songList[0].SongTitle);
                Assert.AreEqual("TestArtistName", songList[0].Artist);

                Assert.AreEqual(2, songList[1].Id);
                Assert.AreEqual("TestSongTitle2", songList[1].SongTitle);
                Assert.AreEqual("TestArtistName", songList[1].Artist);

                Assert.AreEqual(3, songList[2].Id);
                Assert.AreEqual("TestSongTitle3", songList[2].SongTitle);
                Assert.AreEqual("TestArtistName", songList[2].Artist);
            }
        }

        [TestMethod]
        public void ListAllSongs_ThrowsCorrectExceptionWhenDatabaseIsEmpty()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ListAllSongs_ThrowsCorrectExceptionWhenDatabaseIsEmpty")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                ISongRepository repository = new SongRepository(context);

                // Act & Assert
                Assert.ThrowsException<SongNotFoundException>(() =>
                {
                    List<SongListAllViewModel> songList = repository.ListAllSongs();
                });
            }
        }

        [TestMethod]
        public void LinkGenreToSong_LinksGenreToSongCorrectly()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ListAllSongs_ThrowsCorrectExceptionWhenDatabaseIsEmpty")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                context.Songs.Add(new Song()
                {
                    SongId = 1,
                    SongTitle = "TestSong"
                });
                context.Genres.Add(new Genre()
                {
                    GenreId = 1,
                    GenreTitle = "TestGenre"
                });
                context.SaveChanges();

                // Act
                ISongRepository repo = new SongRepository(context);
                repo.LinkGenreToSong(1, 1);
                Genre genre = context.Genres.SingleOrDefault();
                SongSingleViewModel song = new SongSingleViewModel()
                {
                    SongTitle = context.Songs.SingleOrDefault().SongTitle,
                    Genres = context.Songs.SingleOrDefault().Genres
                        .Select(g => g.GenreTitle)
                        .ToArray()
                };

                // Assert
                Assert.AreEqual("TestSong", song.SongTitle);
                Assert.AreEqual(genre.GenreTitle, song.Genres[0]);
                Assert.AreEqual("TestGenre", genre.GenreTitle);
            }
        }

    }
}
