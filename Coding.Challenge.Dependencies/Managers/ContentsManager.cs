using Coding.Challenge.Dependencies.Database;
using Coding.Challenge.Dependencies.Models;

namespace Coding.Challenge.Dependencies.Managers
{
    public class ContentsManager : IContentsManager
    {
        private readonly IDatabase<Content?, ContentDto?> _database;
        public ContentsManager(IDatabase<Content?, ContentDto> database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Content?>> GetAllContents()
        {
            return await _database.ReadAll();
        }

        public async Task<IEnumerable<Content?>> GetFilteredContents(string? title, string? genre)
        {
            try
            {
                var contents = await GetAllContents();

                if (!string.IsNullOrEmpty(title))
                {
                    contents = contents.Where(content => content?.Title.Contains(title, StringComparison.OrdinalIgnoreCase) ?? false);
                }

                if (!string.IsNullOrEmpty(genre))
                {
                    contents = contents.Where(content => content?.GenreList.Any(g => g.Contains(genre, StringComparison.OrdinalIgnoreCase)) ?? false);
                }

                return contents;
            }
            catch (Exception exception)
            {
                return Enumerable.Empty<Content?>();
            }
            
        }

        public async Task<Content?> CreateContent(ContentDto content)
        {
            return await _database.Create(content);
        }

        public async Task<Content?> GetContent(Guid id)
        {
            return await _database.Read(id);
        }

        public async Task<Content?> UpdateContent(Guid id, ContentDto content)
        {
            return await _database.Update(id, content);
        }

        public async Task<Guid> DeleteContent(Guid id)
        {
            return await _database.Delete(id);
        }

        public async Task<Content?> AddGenres(Guid id, IEnumerable<string> genres)
        {
            try
            {
                var content = await _database.Read(id);

                if (content is null)
                    return content;

                var updatedGenres = content.GenreList.Concat(genres)
                    .Distinct(StringComparer.OrdinalIgnoreCase).ToList();

                var updatedContentDto = ConvertContentToContentDto(content, updatedGenres);

                var updatedContent = await _database.Update(id, updatedContentDto);
                return updatedContent;
            }
            catch (Exception exception)
            {
                return null;
            }
            
        }

        public async Task<Content?> RemoveGenres(Guid id, IEnumerable<string> genres)
        {
            try
            {
                var content = await _database.Read(id);

                if (content is null)
                    return null;

                var updatedGenres = content.GenreList
                    .Except(genres, StringComparer.OrdinalIgnoreCase).ToList();

                var updatedContentDto = ConvertContentToContentDto(content, updatedGenres);

                var updatedContent = await _database.Update(id, updatedContentDto);
                return updatedContent;
            }
            catch (Exception exception)
            {
                return null;
            }
            
        }

        private ContentDto ConvertContentToContentDto(Content content, List<string> genres)
        {
            return new ContentDto(
                content.Title,
                content.SubTitle,
                content.Description,
                content.ImageUrl,
                content.Duration,
                content.StartTime,
                content.EndTime,
                genres
            );
        }
    }
}
