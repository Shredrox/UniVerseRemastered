using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LikeController(ILikeService likeService) : ControllerBase
{
    [HttpGet("post/{postId:int}/getLikes")]
    public async Task<IActionResult> GetPostLikes(int postId)
    {
        var likesCount = await likeService.GetPostLikes(postId);
        return Ok(likesCount);
    }
    
    [HttpGet("post/{postId:int}/likedBy/{username}")]
    public async Task<IActionResult> GetPostIsLiked(int postId, string username)
    {
        var isLiked = await likeService.IsPostLiked(postId, username);
        return Ok(isLiked);
    }
    
    [HttpPost("post/like")]
    public async Task<IActionResult> LikePost([FromBody] LikeRequestDto request)
    {
        await likeService.LikePost(request.PostId, request.Username);
        return Ok("Post liked.");
    }
    
    [HttpDelete("post/{postId:int}/unlike")]
    public async Task<IActionResult> UnlikePost(int postId, [FromQuery] string username)
    {
        await likeService.UnlikePost(postId, username);
        return Ok("Post unliked.");
    }
}