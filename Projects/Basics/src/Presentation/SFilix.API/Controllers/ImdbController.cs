using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SFilix.API.RequestModels;
using SFlix.Data;
using SFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.Controllers
{
    [ApiController]
    [Route("/api/v1/imdb")]
    public class ImdbController : ControllerBase
    {
        private SFlixDbContext _dbContext;

        public ImdbController(SFlixDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/getall")]
        public async Task<IActionResult> GetAllAsync(double? rating, int? votes)
        {
            try
            {
                ImdbFilterRequest filterRequest = new ImdbFilterRequest() { Rating = rating, Votes = votes };
                var imdbs = await FilterImdbsRequest(filterRequest)
                    .Select(m => new Imdb()
                    {
                        Id = m.Id,
                        Votes = m.Votes,
                        Rating = m.Rating,
                        MovieId = m.MovieId
                    })
                    .OrderBy(m => m.Votes)
                    .ThenBy(c => c.Rating)
                    .ToListAsync();

                return Ok(imdbs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("/getallWithMovie")]
        public async Task<IActionResult> GetAllWithMovieAsync()
        {
            try
            {
                var imdbs = await _dbContext.Imdbs
                    .AsQueryable()
                    .GroupJoin(
                         _dbContext.Movies,
                        i => i.MovieId,
                        m => m.ImdbId,
                        (r, o) => new { r.Rating, o.First().Title }
                    ).ToListAsync();

                return Ok(imdbs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("/getByVotes")]
        public async Task<IActionResult> GetByVotesAsync()
        {
            try
            {
               // var imdbs = await AggregationFrameworkExample();
               var imdbs = await LinqExample();


                return Ok(imdbs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private async Task<IEnumerable<object>> LinqExample()
        {
            var imdbs = await _dbContext.Imdbs
                    .AsQueryable()
                    .Select(m => new { Votes = m.Votes, Rating = m.Rating })
                    .OrderBy(m => m.Votes)
                    .ThenBy(m => m.Rating)
                    .Select(m => new { BestImdbRating = m.Votes })
                    .ToListAsync();

            return imdbs;
        }

        private async Task<IEnumerable<object>> AggregationFrameworkExample()
        {
            var imdbs = await _dbContext.Imdbs
                    .Aggregate()
                    .Project(m => new { Votes = m.Votes, Rating = m.Rating })
                    .SortByDescending(m => m.Rating)
                    .ThenByDescending(m => m.Votes)
                    .Project(m => new { BestImdbRating = m.Votes })
                    .ToListAsync();

            return imdbs;
        }

        [HttpPost]
        [Route("/addImdb")]
        public async Task<IActionResult> AddImdbAsync([FromBody] AddImdbRequest imdb)
        {
            try
            {
                var newImdb = new Imdb() { Votes = imdb.Votes,  Rating = imdb.Rating, MovieId = imdb.MovieId};

                await _dbContext.Imdbs.InsertOneAsync(newImdb);

                return Ok(newImdb);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private IMongoQueryable<Imdb> FilterImdbsRequest(ImdbFilterRequest filterRequest)
        {
            var filter = _dbContext.Imdbs.AsQueryable();

            if (filterRequest.Rating.HasValue)
            {
                filter.Where(i => i.Rating >= filterRequest.Rating.Value);
            }

            if (filterRequest.Votes.HasValue)
            {
                filter.Where(i => i.Votes <= filterRequest.Votes.Value);
            }

            return filter;
        }
    }
}
