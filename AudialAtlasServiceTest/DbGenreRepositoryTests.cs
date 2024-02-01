using AudialAtlasService.Data;
using AudialAtlasService.Models;
using AudialAtlasService.Models.DTOs;
using AudialAtlasService.Models.ViewModels.GenreViewModels;
using AudialAtlasService.Repositories;
using AudialAtlasService.Repositories.GenreRepoExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudialAtlasServiceTest
{
    [TestClass]
    public class DbGenreRepositoryTests
    {
        // PostGenre Tests

        [TestMethod]
        public void PostGenre_AddGenreSuccessfully()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("PostGenre_AddGenreSuccessfully")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Act
                string genreTitle = "test-tag";
                repository.PostGenre(new GenreDto { GenreTitle = genreTitle });

                // Assert
                Genre createdGenre = context.Genres.Single(g => g.GenreTitle == genreTitle);
                Assert.AreEqual(genreTitle, createdGenre.GenreTitle, "The genre title does not match.");
                Assert.AreEqual(1, context.Genres.Count(), "Unexpected number of genres.");
            }
        }

        [TestMethod]
        public void PostGenre_AttemptToAddDuplicateGenre_ThrowsException()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("PostGenre_AttemptToAddDuplicateGenre_ThrowsException")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Act
                string genreTitle = "test-tag";

                // First call to PostGenre
                repository.PostGenre(new GenreDto { GenreTitle = genreTitle });

                // Assert
                Genre createdGenre = context.Genres.Single(g => g.GenreTitle == genreTitle);
                Assert.AreEqual(genreTitle, createdGenre.GenreTitle, "The genre title does not match.");
                Assert.AreEqual(1, context.Genres.Count(), "Unexpected number of genres.");

                // Act again (second call to PostGenre with the same genreTitle)
                try
                {
                    repository.PostGenre(new GenreDto { GenreTitle = genreTitle });
                    Assert.Fail("Expected GenreAlreadyExistsException was not thrown.");
                }
                catch (GenreAlreadyExistsException ex)
                {
                    // Assert
                    Assert.AreEqual($"Genre with title '{genreTitle}' already exists.", ex.Message, "Unexpected exception message.");
                }

                // Assert (check that there is still only one genre with the specified title)
                Assert.AreEqual(genreTitle, createdGenre.GenreTitle, "The genre title does not match.");
                Assert.AreEqual(1, context.Genres.Count(), "Unexpected number of genres.");
            }
        }

        [TestMethod]
        public void PostGenre_AttemptToAddDuplicateGenre_ThrowsGenreAlreadyExistsException()
        {
            // Arrange
            string genreTitle = "test-tag";

            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("PostGenre_AttemptToAddDuplicateGenre_ThrowsGenreAlreadyExistsException")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Act
                try
                {
                    // First call to PostGenre
                    repository.PostGenre(new GenreDto { GenreTitle = genreTitle });

                    // Attempt to add a duplicate genre
                    repository.PostGenre(new GenreDto { GenreTitle = genreTitle });

                    // Fail the test if the expected exception is not thrown
                    Assert.Fail("Expected GenreAlreadyExistsException was not thrown.");
                }
                catch (GenreAlreadyExistsException ex)
                {
                    // Assert
                    Assert.AreEqual($"Genre with title '{genreTitle}' already exists.", ex.Message, "Unexpected exception message.");
                }
            }
        }

        // ListAllGenre Tests
        [TestMethod]
        public void ListAllGenre_DatabaseWithGenres_ReturnsCorrectList()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ListAllGenre_DatabaseWithGenres_ReturnsCorrectList")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Act
                string genreTitle1 = "Test Genre1";
                string genreTitle2 = "Test Genre2";
                string genreTitle3 = "Test Genre3";

                context.Genres.Add(new Genre { GenreTitle = genreTitle1 });
                context.Genres.Add(new Genre { GenreTitle = genreTitle2 });
                context.Genres.Add(new Genre { GenreTitle = genreTitle3 });
                context.SaveChanges();

                // Assert
                List<GenreListAllViewModel> genres = repository.ListAllGenre();
                Assert.AreEqual(3, genres.Count);
                Assert.IsTrue(genres.Any(g => g.GenreTitle == genreTitle1), "The genre 'test1' was not created.");
                Assert.IsTrue(genres.Any(g => g.GenreTitle == genreTitle2), "The genre 'test2' was not created.");
                Assert.IsTrue(genres.Any(g => g.GenreTitle == genreTitle3), "The genre 'test3' was not created.");
            }
        }

        [TestMethod]
        public void ListAllGenre_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ListAllGenre_EmptyDatabase_ReturnsEmptyList")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Act
                List<GenreListAllViewModel> genres = repository.ListAllGenre();

                // Assert
                Assert.AreEqual(0, genres.Count, "The list should be empty when there are no genres in the database.");
            }
        }

        // GetSingleGenre Tests
        [TestMethod]
        public void GetSingleGenre_GenreExists()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetSingleGenre_GenreExists")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Add a genre to the database
                string genreTitle = "Test Genre";
                Genre addedGenre = new Genre { GenreTitle = genreTitle };
                context.Genres.Add(addedGenre);
                context.SaveChanges();

                // Act
                GenreSingleViewModel result = repository.GetSingleGenre(addedGenre.GenreId);

                // Assert
                Assert.IsNotNull(result, "The returned GenreSingleViewModel should not be null.");
                Assert.AreEqual(genreTitle, result.GenreTitle, "The genre title does not match the expected value.");
            }
        }

        [TestMethod]
        public void GetSingleGenre_GenreDoesNotExist()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetSingleGenre_GenreDoesNotExist")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Choose a non-existing genreId
                int nonExistingGenreId = 420;

                // Act
                GenreSingleViewModel result = repository.GetSingleGenre(nonExistingGenreId);

                // Assert
                Assert.IsNull(result, "The returned GenreSingleViewModel should be null for a non-existing genreId.");
            }
        }

        // GetAllArtistsInGenre Tests
        [TestMethod]
        public void GetAllArtistsInGenre_GenreExistsWithArtists()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetAllArtistsInGenre_GenreExistsWithArtists")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Add a genre with associated artists to the database
                string genreTitle = "Test Genre";
                Genre addedGenre = new Genre { GenreTitle = genreTitle };
                Artist artist1 = new Artist { Name = "Artist 1", Description = "Description 1" };
                Artist artist2 = new Artist { Name = "Artist 2", Description = "Description 2" };
                addedGenre.Artists = new List<Artist> { artist1, artist2 };

                context.Genres.Add(addedGenre);
                context.SaveChanges();

                // Act
                List<ArtistsInGenreViewModel> result = repository.GetAllArtistsInGenre(addedGenre.GenreId);

                // Assert that the result is not null
                Assert.IsNotNull(result, "The result should not be null.");

                // Assert that the result contains exactly one element
                Assert.AreEqual(1, result.Count, "The result should contain exactly one element.");

                // Assert that the GenreTitle matches the expected value
                Assert.AreEqual(genreTitle, result[0].GenreTitle, "Incorrect GenreTitle.");

                // Assert that there are exactly two artists in the result
                Assert.AreEqual(2, result[0].Artists.Count, "The number of associated artists should be 2.");

                // Optionally, you can assert specific properties of the artists if needed
                Assert.AreEqual("Artist 1", result[0].Artists[0].Name, "Incorrect name for the first artist.");
                Assert.AreEqual("Artist 2", result[0].Artists[1].Name, "Incorrect name for the second artist.");
            }
        }

    }
}
