using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.RequestModels
{
    [BsonIgnoreExtraElements]
    public class LookupDemo
    {
        public IList<ImdbWithMovieResponse> Models { get; set; }
    }
}
