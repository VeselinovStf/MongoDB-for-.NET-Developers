using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SFlix.Data;
using SFlix.Models;
using System;
using System.Threading.Tasks;

namespace SFilix.API.Controllers
{
    [ApiController]
    [Route("/api/v1/comment")]
    public class CommentsController : ControllerBase
    {
        private SFlixDbContext _dbContext;

        public CommentsController(SFlixDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/getallComments")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var coments = await _dbContext
                    .Comments
                    .Find(new BsonDocument())
                    .SortBy(m => m.Text.Length)
                    .ThenByDescending(c => c.Author.Length)
                    .ThenByDescending(c => c.Id)
                    .ToListAsync();

                return Ok(coments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("/getCommentbyAuthorName")]
        public async Task<IActionResult> GetByAuthorNameAsync(string authorName)
        {
            try
            {
                var coments = await _dbContext
                    .Comments
                    .Find(Builders<Comment>.Filter.Where(c => c.Author == authorName))
                    .FirstOrDefaultAsync();

                return Ok(coments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
