using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using SFilix.Data;
using SFlix.Models;

namespace SFlix.Data
{
    public class SFlixDbContext
    {
        private IMongoDatabase Database { get; set; }

        public SFlixDbContext(IOptions<DbSetting> dbSetting)
        {
            var client = new MongoClient(dbSetting.Value.ConnectionString);
            var database = client.GetDatabase(dbSetting.Value.DatabaseName);

            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

            Database = database;
        }

        public IMongoCollection<Movie> Movies 
        { 
            get 
            {
                return Database.GetCollection<Movie>("movies"); 
            }
        }
       
    }
}
