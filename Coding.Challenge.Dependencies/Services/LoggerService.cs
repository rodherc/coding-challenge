using Coding.Challenge.Dependencies.Database.Interfaces;
using Coding.Challenge.Dependencies.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Coding.Challenge.Dependencies.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration _configuration;

        public LoggerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IMongoDatabase GetDatabase()
        {
            var settings = MongoClientSettings.FromConnectionString(
                $"mongodb://{_configuration["SettingsCacheCore:ServerAdress"]}:{_configuration["SettingsCacheCore:Port"]}");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            return new MongoClient(settings)
                .GetDatabase(_configuration["SettingsCacheCore:DatabaseName"]);
        }

        private IMongoCollection<CacheLog> GetLogCollection()
        {
            var database = GetDatabase();
            return database.GetCollection<CacheLog>("LogsInfo");
        }

        public async Task LogAsync(string key, string content)
        {
            var logEntry = new CacheLog
            {
                Key = key,
                Content = content,
                CreateAt = DateTimeOffset.UtcNow
            };

            var logCollection = GetLogCollection();
            await logCollection.InsertOneAsync(logEntry);
        }
    }
}
