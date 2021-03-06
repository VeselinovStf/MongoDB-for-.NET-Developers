﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SFlix.Models
{
    public class Imdb
    {
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

        [BsonElement("rating")]
        public double Rating { get; set; }

        [BsonElement("votes")]
        public int Votes { get; set; }

        [BsonElement("movieId")]
        public string MovieId { get; set; }
    }
}
