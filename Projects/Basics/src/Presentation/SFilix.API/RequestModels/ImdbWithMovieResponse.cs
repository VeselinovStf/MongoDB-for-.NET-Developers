using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.RequestModels
{
    [BsonIgnoreExtraElements]
    public class ImdbWithMovieResponse
    {
        public string Title { get; set; }

        public double Rating { get; set; }


        public int Votes { get; set; }
    }
}
