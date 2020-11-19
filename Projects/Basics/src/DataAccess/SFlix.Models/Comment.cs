using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SFlix.Models
{
    public class Comment
    {
        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        public ObjectId Id { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }
    }
}
