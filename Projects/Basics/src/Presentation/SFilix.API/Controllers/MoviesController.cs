using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SFilix.API.RequestModels;
using SFlix.Data;
using SFlix.Models;
using System;

using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.Controllers
{
    [ApiController]
    [Route("/api/v1/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly SFlixDbContext _dbContext;
      
        public MoviesController(SFlixDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        [HttpGet]
        [Route("/getallmovies")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var movies = await _dbContext.Movies.Find(new BsonDocument()).ToListAsync();

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("/getmoviebyname")]
        public async Task<IActionResult> GetByNameAsync(string movieTitle)
        {
            try
            {

                var movieFilter = Builders<Movie>.Filter.Eq(m => m.Title, movieTitle);

                var movie = await _dbContext.Movies.Find(movieFilter).FirstAsync();
              
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("/addmovie")]
        public async Task<IActionResult> AddMoviewAsync([FromBody] AddMovieRequest movie)
        {
            try
            {
                await _dbContext.Movies.InsertOneAsync(new Movie() { Title = movie.Title, Plot = movie.Plot, Year = movie.Year});

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/updatemovie")]
        public async Task<IActionResult> UpdateMoviewAsync([FromBody] UpdateMovieRequest movie)
        {
            try
            {
                var filter = Builders<Movie>.Filter.Eq(m => m.Id, movie.Id);

                var updateDifinition = new BsonDocument()
            {
                {
                    "$set", new BsonDocument()
                    {
                        {"title", movie.Title },
                        {"plot", movie.Plot},
                        {"year", movie.Year},
                        {"imdbId", movie.ImdbId }
                    }
                }
            };

                var updateOptions = new UpdateOptions();
                updateOptions.IsUpsert = movie.InsertIfNotExist;

                var update = await _dbContext.Movies.UpdateOneAsync(filter, updateDifinition, updateOptions);

                var updated = await this._dbContext.Movies.FindAsync(filter);

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/deletemovie")]
        public async Task<IActionResult> DeleteMoviewAsync([FromBody] string id)
        {
            try
            {
                var filter = Builders<Movie>.Filter.Eq(m => m.Id, id);

                await _dbContext.Movies.DeleteOneAsync(filter);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
