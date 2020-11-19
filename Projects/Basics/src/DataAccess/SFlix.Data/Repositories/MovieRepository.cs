using MongoDB.Bson;
using MongoDB.Driver;
using SFlix.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFlix.Data.Repositories
{
    public class MovieRepository 
    {
        private readonly SFlixDbContext _dbContext;

        public MovieRepository(SFlixDbContext dbContext)
        {
            _dbContext = dbContext;           
        }    

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _dbContext.Movies.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddMovieAsync(string title, string plot, int year)
        {
            await _dbContext.Movies.InsertOneAsync(new Movie() { Title = title, Plot = plot, Year = year });
        }

        public async Task<Movie> GetByTitleAsync(string movieTitle)
        {
            var movieFilter = Builders<Movie>.Filter.Eq(m => m.Title ,movieTitle);

            return await _dbContext.Movies.Find(movieFilter).FirstAsync();
        }

        public async Task<Movie> GetByIdAsync(string id)
        {
            var movieFilter = Builders<Movie>.Filter.Eq(m => m.Id, id);

            return await _dbContext.Movies.Find(movieFilter).FirstAsync();
        }

        public async Task<Movie> UpdateMovieAsync(string id, string title, string plot, int year, bool upsert)
        {
            var filter = Builders<Movie>.Filter.Eq(m => m.Id, id);

            var updateDifinition = new BsonDocument()
            {
                {
                    "$set", new BsonDocument()
                    {
                        {"title", title },
                        {"plot", plot},
                        {"year", year},
                    }
                }
            };

            var updateOptions = new UpdateOptions();
            updateOptions.IsUpsert = upsert;

            var update = await _dbContext.Movies.UpdateOneAsync(filter, updateDifinition, updateOptions);
           

            if (update.IsAcknowledged)
            {
                return await GetByIdAsync(id);
            }
            else
            {
                throw new Exception("Can't Update");
            }
        }
    }
}
