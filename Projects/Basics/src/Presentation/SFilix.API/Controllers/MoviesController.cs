using Microsoft.AspNetCore.Mvc;
using SFilix.API.RequestModels;
using SFlix.Data.Repositories;
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
        private readonly MovieRepository _movieRepository;

        public MoviesController(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        [Route("/getallmovies")]
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

        [HttpGet]
        [Route("/getmoviebyname")]
        public async Task<IActionResult> GetByNameAsync(string movieTitle)
        {
            try
            {
                var movie = await _movieRepository.GetByTitleAsync(movieTitle);

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
                await _movieRepository.AddMovieAsync(movie.Title, movie.Plot,movie.Year);

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/updatemovie")]
        public async Task<IActionResult> AddMoviewAsync([FromBody] UpdateMovieRequest movie)
        {
            try
            {
                var movieUpdate = await _movieRepository.UpdateMovieAsync(
                    movie.Id, movie.Title, movie.Plot, movie.Year, movie.InsertIfNotExist);

                return Ok(movieUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
