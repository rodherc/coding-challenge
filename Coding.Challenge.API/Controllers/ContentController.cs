using Coding.Challenge.API.Models;
using Coding.Challenge.Dependencies.Database.Interfaces;
using Coding.Challenge.Dependencies.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Coding.Challenge.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContentController : Controller
    {
        private readonly IContentsManager _manager;
        private readonly ILoggerService _logger;
        public ContentController(IContentsManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        [Obsolete("This endpoint is deprecated. Use GET /api/v1/Content/filter instead.")]
        public async Task<IActionResult> GetManyContents()
        {
            var contents = await _manager.GetAllContents().ConfigureAwait(false);

            if (!contents.Any())
            {
                await _logger.LogAsync("GetManyContents", "No contents found");
                return NotFound();
            }
            await _logger.LogAsync("GetManyContents", "Getting all contents");

            return Ok(contents);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredContents([FromQuery] string? title, [FromQuery] string? genre)
        {
            var contents = await _manager.GetFilteredContents(title, genre).ConfigureAwait(false);

            if (!contents.Any())
            {
                await _logger.LogAsync("GetFilteredContents", "No contents found with the given filters")
                    .ConfigureAwait(false);

                return NotFound();
            }

            await _logger.LogAsync("GetFilteredContents", $"Filtering contents by title: {title} and genre: {genre}")
                .ConfigureAwait(false);

            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContent(Guid id)
        {
            var content = await _manager.GetContent(id).ConfigureAwait(false);

            if (content == null)
            {
                await _logger.LogAsync("GetContent", $"Content with id {id} not found")
                    .ConfigureAwait(false);

                return NotFound();
            }

            await _logger.LogAsync("GetContent", $"Getting content with id {id}")
                .ConfigureAwait(false);

            return Ok(content);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContent([FromBody] ContentInput content)
        {
            var createdContent = await _manager.CreateContent(content.ToDto()).ConfigureAwait(false);

            if (createdContent == null)
            {
                await _logger.LogAsync("CreateContent", "Failed to create content")
                    .ConfigureAwait(false);

                return Problem();
            }

            await _logger.LogAsync("CreateContent", $"Content created with id {createdContent.Id}")
                .ConfigureAwait(false);

            return createdContent == null ? Problem() : Ok(createdContent);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateContent(Guid id, [FromBody] ContentInput content)
        {
            var updatedContent = await _manager.UpdateContent(id, content.ToDto()).ConfigureAwait(false);

            if (updatedContent == null)
            {
                await _logger.LogAsync("UpdateContent", $"Content with id {id} not found")
                    .ConfigureAwait(false);

                return NotFound();
            }

            await _logger.LogAsync("UpdateContent", $"Content with id {id} updated")
                .ConfigureAwait(false);

            return updatedContent == null ? NotFound() : Ok(updatedContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(Guid id)
        {
            var deletedId = await _manager.DeleteContent(id).ConfigureAwait(false);

            await _logger.LogAsync("DeleteContent", $"Content with id {id} deleted")
                .ConfigureAwait(false);

            return Ok(deletedId);
        }

        /// <summary>
        /// Inserts new genres into a specific content according to the id passed.
        /// </summary>
        /// <param name="contentId">Content Id.</param>
        /// <param name="genres">List of genres to be added.</param>
        /// <returns>Returns the updated genre list or the error that occurred while updating the list</returns>
        [HttpPost("{contentId}/genre")]
        public async Task<IActionResult> AddGenres(Guid contentId, [FromBody] IEnumerable<string> genres)
        {
            var result = await _manager.AddGenres(contentId, genres).ConfigureAwait(false);

            if (result is null)
            {
                await _logger.LogAsync("AddGenres", $"Failed to add genres to content with id {contentId}")
                    .ConfigureAwait(false);

                return Problem();
            }

            await _logger.LogAsync("AddGenres", $"Genres added to content with id {contentId}")
                .ConfigureAwait(false);

            return Ok(result.GenreList);
        }

        /// <summary>
        /// Removes genres into a specific content according to the id passed.
        /// </summary>
        /// <param name="contentId">Content Id.</param>
        /// <param name="genres">List of genres to be removed.</param>
        /// <returns>Returns the updated genre list or the error that occurred while updating the list</returns>
        [HttpDelete("{contentId}/genre")]
        public async Task<IActionResult> RemoveGenres(Guid contentId, [FromBody] IEnumerable<string> genres)
        {
            var result = await _manager.RemoveGenres(contentId, genres).ConfigureAwait(false);

            if (result is null)
            {
                await _logger.LogAsync("RemoveGenres", $"Failed to remove genres from content with id {contentId}")
                    .ConfigureAwait(false);

                return Problem();
            }

            await _logger.LogAsync("RemoveGenres", $"Genres removed from content with id {contentId}")
                .ConfigureAwait(false);

            return Ok(result.GenreList);
        }
    }
}
