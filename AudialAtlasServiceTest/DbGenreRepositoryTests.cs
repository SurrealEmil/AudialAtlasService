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
        [TestMethod]
        public void PostGenre_CreateGenre()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("PostGenre_CreateGenre")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Act
                string genreTitle = "test-tag";
                repository.PostGenre(new GenreDto { GenreTitle = genreTitle });

                // Assert
                List<GenreListAllViewModel> genres = repository.ListAllGenre();
                Assert.IsTrue(genres.Any(g => g.GenreTitle == genreTitle), "The genre was not created.");
            }
        }

        [TestMethod]
        public void PostGenre_CreateGenreIfGenreNotAlreadyTaken()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("PostGenre_CreateGenreIfGenreNotAlreadyTaken")
                .Options;

            using (ApplicationContext context = new ApplicationContext(options))
            {
                IGenreRepository repository = new GenreRepository(context);

                // Act
                string genreTitle = "test-tag";

                // First call to PostGenre
                repository.PostGenre(new GenreDto { GenreTitle = genreTitle });

                // Assert
                List<GenreListAllViewModel> genres = repository.ListAllGenre();
                Assert.AreEqual(1, genres.Count(g => g.GenreTitle == genreTitle), "The first genre was not created.");

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
                genres = repository.ListAllGenre();
                Assert.AreEqual(1, genres.Count(g => g.GenreTitle == genreTitle), "Multiple genres with the same title were created.");
            }
        }

        [TestMethod]
        public void PostGenre_AttemptToAddDuplicateGenre_ThrowsGenreAlreadyExistsException()
        {
            // Arrange
            string genreTitle = "test-tag";
            string expectedErrorMessage = $"Genre with title '{genreTitle}' already exists.";

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
                    Assert.AreEqual(expectedErrorMessage, ex.Message, "Unexpected exception message.");
                }
            }
        }

    }
}
