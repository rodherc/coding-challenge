using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Coding.Challenge.Dependencies.Models
{
    public class CacheLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string Key { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreateAt { get; set; }
    }
}
