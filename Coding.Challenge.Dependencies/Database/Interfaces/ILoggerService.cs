
namespace Coding.Challenge.Dependencies.Database.Interfaces
{
    public interface ILoggerService
    {
        Task LogAsync(string key, string content);
    }
}
