using Microsoft.AspNetCore.Mvc;
using SFlix.Data.Abstraction;
using SFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.Controllers
{
    [ApiController]
    [Route("/api/v1/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IRepository<Movie> _movieRepository;

        public MoviesController(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var movies = await _movieRepository.GetAllAsync();

                return Ok(movies.Take(5));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
