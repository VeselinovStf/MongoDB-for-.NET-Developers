using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using SFilix.Data;
using SFlix.Models;

namespace SFlix.Data
{
    public class SFlixDbContext
    {
        private IMongoDatabase Database { get; set; }

        public SFlixDbContext(IOptions<DbSetting> dbSetting)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(dbSetting.Value.ConnectionString));

            //Better solution is to inject ILogger - for ease i use cwl
            settings.ClusterConfigurator = builder => builder.Subscribe(new MongoClientCommandEventHandler());
          
            var client = new MongoClient(settings);
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

        public IMongoCollection<Comment> Comments
        {
            get
            {
                return Database.GetCollection<Comment>("comments");
            }
        }

        public IMongoCollection<Imdb> Imdbs
        {
            get
            {
                return Database.GetCollection<Imdb>("imdbs");
            }
        }

    }
}
