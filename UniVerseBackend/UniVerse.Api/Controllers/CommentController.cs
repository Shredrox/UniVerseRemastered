using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CommentController(ICommentService commentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        return Ok(await commentService.GetAllComments());
    }
    
    [HttpGet("{commentId:int}")]
    public async Task<IActionResult> GetCommentById(int commentId)
    {
        return Ok(await commentService.GetCommentById(commentId));
    }
    
    [HttpGet("{postId:int}/comments")]
    public async Task<IActionResult> GetPostComments(int postId)
    {
        return Ok(await commentService.GetPostComments(postId));
    }
    
    [HttpGet("{postId:int}/comments/count")]
    public async Task<IActionResult> GetPostCommentsCount(int postId)
    {
        return Ok(await commentService.GetPostCommentsCount(postId));
    }
    
    [HttpGet("{commentId:int}/replies")]
    public async Task<IActionResult> GetCommentReplies(int commentId)
    {
        return Ok(await commentService.GetCommentReplies(commentId));
    }
    
    [HttpPost("add-comment")]
    public async Task<IActionResult> GetCommentReplies([FromBody] AddCommentRequestDto request)
    {
        await commentService.CreateComment(request);
        return Ok("Comment added.");
    }
    
    [HttpPost("{commentId:int}/add-reply")]
    public async Task<IActionResult> AddReply(int commentId, [FromBody] AddCommentRequestDto request)
    {
        await commentService.AddReply(commentId, request);
        return Ok("Reply added.");
    }
    
    [HttpPut("{commentId:int}")]
    public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentRequestDto request)
    {
        await commentService.UpdateComment(commentId, request);
        return Ok("Comment updated.");
    }
    
    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        await commentService.DeleteComment(commentId);
        return Ok("Comment deleted.");
    }
}