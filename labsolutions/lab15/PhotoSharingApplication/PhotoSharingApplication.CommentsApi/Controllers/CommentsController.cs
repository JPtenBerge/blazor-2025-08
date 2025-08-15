using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApplication.CommentsApi.Core.Interfaces;
using PhotoSharingApplication.CommentsApi.Core.Models;

namespace PhotoSharingApplication.CommentsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(ICommentsRepository _commentsRepository) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        Comment? comment = await _commentsRepository.GetCommentByIdAsync(id);

        if (comment is null)
        {
            return NotFound();
        }

        return comment;
    }

    [HttpGet("/api/photos/{photoId}/comments")]
    public async Task<IEnumerable<Comment>> GetCommentsByPhotoId(int photoId)
    {
        return await _commentsRepository.GetCommentsForPhotoAsync(photoId);
    }


    [Authorize]
    [HttpPut("/apiauth/comments/{id}")]
    public async Task<IActionResult> PutComment(int id, Comment comment)
    {
        if (id != comment.Id)
        {
            return BadRequest();
        }

        await _commentsRepository.UpdateCommentAsync(comment);

        return NoContent();
    }

    [Authorize]
    [HttpPost("/apiauth/comments/")]
    public async Task<ActionResult<Comment>> PostComment(Comment comment)
    {
        await _commentsRepository.AddCommentAsync(comment);

        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    [Authorize]
    [HttpDelete("/apiauth/comments/{id}")]
    public async Task<ActionResult<Comment>> DeleteComment(int id)
    {
        var comment = await _commentsRepository.GetCommentByIdAsync(id);
        if (comment is null)
        {
            return NotFound();
        }

        await _commentsRepository.DeleteCommentAsync(id);

        return comment;
    }
}
