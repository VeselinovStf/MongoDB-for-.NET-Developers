using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SFlix.Models
{
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
        public object Year { get; set; }

        [BsonElement("cast")]
        public List<string> Cast { get; set; }

        [BsonElement("plot")]
        public string Plot { get; set; }

        [BsonElement("fullplot")]
        public string FullPlot { get; set; }

        [BsonElement("lastupdated")]
        public object LastUpdated { get; set; }

        [BsonElement("released")]
        public DateTime Released { get; set; }

        [BsonElement("rated")]
        public string Rated { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("poster")]
        public string Poster { get; set; }

        [BsonElement("directors")]
        public List<string> Directors { get; set; }

        [BsonElement("writers")]
        public List<string> Writers { get; set; }

        [BsonElement("imdb")]
        public Imdb Imdb { get; set; }

        [BsonElement("countries")]
        public List<string> Countries { get; set; }

        [BsonElement("genres")]
        public List<string> Genres { get; set; }

        [BsonElement("runtime")]
        public int Runtime { get; set; }

        [BsonElement("tomatoes")]
        public RottenTomatoes Tomatoes { get; set; }

        [BsonElement("comments")]
        public List<Comment> Comments
        {
            get { return comments != null ? comments.OrderByDescending(c => c.Date).ToList() : new List<Comment>(); }
            set => comments = value;
        }

        [BsonElement("num_mflix_comments")]
        public int NumberOfComments { get; set; }

        [BsonElement("awards")]
        public Awards Awards { get; set; }

        [BsonElement("languages")]
        public List<string> Languages { get; set; }

        [BsonElement("metacritic")]
        public int? MetacriticScore { get; set; }
    }
}
