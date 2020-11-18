using MongoDB.Bson.Serialization.Attributes;

namespace SFlix.Models
{
    public class Rating
    {
        [BsonElement("rating")]
        public double RatingScore { get; set; }

        public int NumReviews { get; set; }

        public int Meter { get; set; }
    }
}
