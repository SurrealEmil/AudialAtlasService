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

        [TestMethod]
        [ExpectedException(typeof(SongFailedToAddToDatabaseException))]
        public void PostSong_ThrowsCorrectException() 
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("PostSong_ThrowsCorrectException")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {                
                ISongRepository repository = new SongRepository(context);

                // Act
                repository.PostSong(1, new SongDto
                {
                    SongTitle = "TestSongTitle"
                });
            }
        }

    }
}
