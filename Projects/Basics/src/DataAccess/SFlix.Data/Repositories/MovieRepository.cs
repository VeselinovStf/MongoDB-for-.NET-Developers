using MongoDB.Bson;
using MongoDB.Driver;
using SFlix.Data.Abstraction;
using SFlix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFlix.Data.Repositories
{
    public class MovieRepository : IRepository<Movie>
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
    }
}
