using AtlasConnectionString;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HelloMongoDb
{
    // Conect to Db/Atlas Cluster
    public class StartUp
    {
        public static void Main(string[] args)
        { 

            var mongoClient = new MongoClient(AtlasConnection.ConnectionString);

            var database = mongoClient.GetDatabase("sample_mflix");
            var collection = database.GetCollection<BsonDocument>("movies");

            var movie = collection.Find("{ title: \"The Birth of a Nation\" }").FirstOrDefault();

            System.Console.WriteLine(movie);
        }
    }
}
