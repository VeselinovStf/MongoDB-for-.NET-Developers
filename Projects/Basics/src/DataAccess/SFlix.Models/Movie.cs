using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SFlix.Models
{
    [BsonIgnoreExtraElements]
    public class Movie
    {
        private List<Comment> comments;
        private string _id;

        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("plot")]
        public string Plot { get; set; }

       
    }
}
