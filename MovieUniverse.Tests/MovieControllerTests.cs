using NUnit.Framework;
using MovieUniverse.WebAPI;
using MovieUniverse.WebAPI.Controllers;
using Moq;
using MovieUniverse.Data;
using MovieUniverse.Data.Models;
using NUnit.Framework.Internal;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace MovieUniverse.Tests
{
    public class MovieControllerTests
    {
        private Mock<ILogger<MoviesController>> loggerMock;
        private Mock<IMovieRepository> movieRepositoryMock;
        private IEnumerable<Movie> movies;
        private MoviesController controller;

        [SetUp]
        public void Setup()
        {
            loggerMock = new Mock<ILogger<MoviesController>>();
            movieRepositoryMock = new Mock<IMovieRepository>();
            
            controller = new MoviesController(loggerMock.Object, movieRepositoryMock.Object);
        }
               

        [Test]
        public async Task GetAll_Valid_ReturnsAllMovies()
        {
            //Arrange
            AddMovieToList();
            movieRepositoryMock.Setup(a=> a.GetAll()).Returns(Task.FromResult(movies));

            //Act
            var actual = await controller.GetAll();

            //Assert
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().BeEquivalentTo(movies);
            movieRepositoryMock.Verify(a => a.GetAll(), Times.Once);
        }

        [Test]
        public async Task GetById_ExistingMovie_ReturnExpectedMovie()
        {
            //Arrange
            var expacted = new Movie
            {
                Id = 4,
                Title = "SUperbad",
                ShortDescription = "Comedy",
                ReleaseDate = new DateTime(2008, 8, 9),
                Genres = new List<Genre> { Genre.Comedy }
            };
            movieRepositoryMock.Setup(m => m.GetMovieById(It.Is<int>(t => t == 4))).Returns(Task.FromResult(expacted));

            //Act
            var actual = await controller.GetMovieById(4);

            //Assert
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().BeAssignableTo<Movie>().And.Be(expacted);
            movieRepositoryMock.Verify(a=> a.GetMovieById(4),Times.Once);
        }

        [Test]
        public async Task GetById_NonExistingMovie_ReturnNotFound()
        {
            //Arrange
            int notExistingId = 15;

            //Act
            var actual = await controller.GetMovieById(notExistingId);

            //Assert
            actual.Should().NotBeNull();
            actual.Should().BeOfType<NotFoundResult>();
            movieRepositoryMock.Verify(a => a.GetMovieById(15), Times.Once);
        }


        [Test]
        public async Task Post_Valid_ReturnSeccess()
        {
            //Arrange
            var created = new Movie
            {
                Id = 4,
                Title = "SUperbad",
                ShortDescription = "Comedy",
                ReleaseDate = new DateTime(2008, 8, 9),
                Genres = new List<Genre> { Genre.Comedy }
            };
            movieRepositoryMock.Setup(m => m.Create(created)).Returns(Task.FromResult(created));

            //Act
            var actual = await controller.Post(created);

            //Assert
            actual.Should().NotBeNull();
            actual.Should().BeOfType<CreatedAtRouteResult>().Subject.Value
                .Should().BeAssignableTo<Movie>().And.Be(created);
            movieRepositoryMock.Verify(a => a.Create(created), Times.Once);
        }

        [Test]
        public async Task Delete_Valid_ReturnNoContent()
        {
            //Arrange
            int deleteId = 4;
            movieRepositoryMock.Setup(m => m.Delete(deleteId)).Returns(Task.FromResult<bool>(true));            

            //Act
            var actual = await controller.Delete(deleteId);

            //Assert
            actual.Should().NotBeNull();
            actual.Should().BeOfType<NoContentResult>();
            movieRepositoryMock.Verify(a => a.Delete(deleteId), Times.Once);
        }

        [Test]
        public async Task Delete_NotFound_ReturnNotFound()
        {
            //Arrange
            int deleteId = 15;

            //Act
            var actual = await controller.Delete(deleteId);

            //Assert
            actual.Should().NotBeNull();
            actual.Should().BeOfType<NotFoundResult>();
            movieRepositoryMock.Verify(a => a.Delete(deleteId), Times.Once);
        }

        private void AddMovieToList()
        {
            movies = new List<Movie>{
                new Movie { Id = 1, Title = "Breaking Bad", ShortDescription = "Interesting sory", ReleaseDate = new DateTime(2008, 8, 9), Genres = new List<Genre> { Genre.Action, Genre.Mistery } },
                new Movie { Id = 2, Title = "Black list", ShortDescription = "CIA,BIA", ReleaseDate = new DateTime(2011, 1, 9), Genres = new List<Genre> { Genre.Action, Genre.Mistery } },
                new Movie { Id = 3, Title = "Notebook", ShortDescription = "Love story", ReleaseDate = new DateTime(2008, 8, 9), Genres = new List<Genre> { Genre.Romance, Genre.Family } },
                new Movie { Id = 4, Title = "SUperbad", ShortDescription = "Comedy", ReleaseDate = new DateTime(2008, 8, 9), Genres = new List<Genre> { Genre.Comedy } }
            };
        }
    }
}