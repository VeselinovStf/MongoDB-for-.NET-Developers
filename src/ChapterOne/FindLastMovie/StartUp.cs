using AtlasConnectionString;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace FindLastMovie
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var mongoClient = new MongoClient(AtlasConnection.ConnectionString);

            var database = mongoClient.GetDatabase("sample_mflix");
            var collection = database.GetCollection<BsonDocument>("movies");

            var lastTenMovies = collection.Find(new BsonDocument())
                .SortByDescending(m => m["runtime"])
                .Limit(10)
                .ToList();

            var lastMovie = lastTenMovies.LastOrDefault();

            Console.WriteLine(lastMovie["title"]);
        }
    }
}
