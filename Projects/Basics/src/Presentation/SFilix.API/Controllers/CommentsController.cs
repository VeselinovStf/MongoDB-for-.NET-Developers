using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SFilix.API.RequestModels;
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
                    .Project(m => new CommentsProjectionDemoResponse()
                    {
                        Id = m.Id,
                        Author = m.Author,
                        Text = m.Text
                    })
                    .SortBy(m => m.Id)
                    .ThenBy(c => c.Id)
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
                    .SortByDescending(c => c.Id)
                    .ToListAsync();

                return Ok(coments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("/getCommentbyId")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            try
            {
                var coments = await _dbContext
                    .Comments
                    .Find(m => m.Id == id)
                    .SortByDescending(c => c.Id)
                    .ToListAsync();

                return Ok(coments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("/addComment")]
        public async Task<IActionResult> AddCommentAsync([FromBody] CommentRequest comment)
        {
            try
            {
                var newComent = new Comment() { Text = comment.Text, Author = comment.Author };
                await _dbContext.Comments.InsertOneAsync(newComent);

                return Ok(newComent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch]
        [Route("/updateAuthorName")]
        public async Task<IActionResult> UpdateAuthorNameAsync([FromBody] UpdateAuthorNameRequest nameRequest)
        {
            try
            {

                var coment = await GetCommentById(nameRequest.Id);

                coment.Author = nameRequest.AuthorName;

                var updated = await _dbContext.Comments.FindOneAndReplaceAsync(c => c.Id == nameRequest.Id, coment);

                coment = await GetCommentById(nameRequest.Id);

                return Ok(coment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("/updateComent")]
        public async Task<IActionResult> UpdateComentAsync([FromBody] UpdateCommentRequest comentUpdate)
        {
            try
            {
                var modificationUpdate = Builders<Comment>.Update
                    .Set(r => r.Text, comentUpdate.Text)
                    .Set(c => c.Author, comentUpdate.Author);

                var option = new UpdateOptions()
                {
                    IsUpsert = comentUpdate.InsertIfNotExists
                };

                await _dbContext.Comments.UpdateOneAsync(c => c.Id == comentUpdate.Id, modificationUpdate, option);

                var coment = await GetCommentById(comentUpdate.Id);

                return Ok(coment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete]
        [Route("/deleteComent")]
        public async Task<IActionResult> DeleteComentAsync(string id)
        {
            try
            {
                await _dbContext.Comments.DeleteOneAsync(c => c.Id == id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private async Task<Comment> GetCommentById(string id)
        {
            return await _dbContext
                   .Comments
                   .Find(c => c.Id == id)
                   .FirstOrDefaultAsync();
        }
    }
}
