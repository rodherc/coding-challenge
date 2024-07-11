using Coding.Challenge.API.Controllers;
using Coding.Challenge.Dependencies.Database;
using Coding.Challenge.Dependencies.Database.Interfaces;
using Coding.Challenge.Dependencies.Managers;
using Coding.Challenge.Dependencies.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Concurrent;
using Xunit;

namespace UnitTests
{
    public class ContentControllerTests
    {
        private readonly Mock<IContentsManager> _mockManager;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly ContentController _controller;

        public ContentControllerTests()
        {
            _mockManager = new Mock<IContentsManager>();
            _mockLogger = new Mock<ILoggerService>();
            _controller = new ContentController(_mockManager.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task AddGenres_ShouldReturnOk_WhenGenresAddedSuccessfully()
        {
            var contentId = Guid.NewGuid();
            var genres = new List<string> { "Genre1", "Genre2" };
            _mockManager.Setup(m => m.AddGenres(contentId, genres));

            var result = await _controller.AddGenres(contentId, genres);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
            _mockLogger.Verify(l => l.LogAsync("AddGenres", It.IsAny<string>()), Times.Exactly(1));
            _mockManager.Verify(m => m.AddGenres(contentId, genres), Times.Once);
        }

        [Fact]
        public async Task AddGenres_ShouldReturnProblem_WhenGenresAdditionFails()
        {
            var contentId = Guid.NewGuid();
            var genres = new List<string> { "Genre1", "Genre2" };
            _mockManager.Setup(m => m.AddGenres(contentId, genres));

            var result = await _controller.AddGenres(contentId, genres);

            var problemResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, problemResult.StatusCode);
            _mockLogger.Verify(l => l.LogAsync("AddGenres", It.IsAny<string>()), Times.Exactly(1));
            _mockManager.Verify(m => m.AddGenres(contentId, genres), Times.Once);
        }

        [Fact]
        public async Task RemoveGenres_ShouldReturnOk_WhenGenresRemovedSuccessfully()
        {
            var contentId = Guid.NewGuid();
            var genres = new List<string> { "Genre1", "Genre2" };
            _mockManager.Setup(m => m.RemoveGenres(contentId, genres));

            var result = await _controller.RemoveGenres(contentId, genres);

            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            _mockLogger.Verify(l => l.LogAsync("RemoveGenres", It.IsAny<string>()), Times.Exactly(1));
            _mockManager.Verify(m => m.RemoveGenres(contentId, genres), Times.Once);
        }

        [Fact]
        public async Task RemoveGenres_ShouldReturnProblem_WhenGenresRemovalFails()
        {
            var contentId = Guid.NewGuid();
            var genres = new List<string> { "Genre1", "Genre2" };
            _mockManager.Setup(m => m.RemoveGenres(contentId, genres));

            var result = await _controller.RemoveGenres(contentId, genres);

            var problemResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, problemResult.StatusCode);
            _mockLogger.Verify(l => l.LogAsync("RemoveGenres", It.IsAny<string>()), Times.Exactly(1));
            _mockManager.Verify(m => m.RemoveGenres(contentId, genres), Times.Once);
        }
    }
}
