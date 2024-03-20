using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PostController(
    IPostService postService,
    IFriendshipService friendshipService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        return Ok(await postService.GetAllPosts());
    }
        
    [HttpGet("user/{username}/count")]
    public async Task<IActionResult> GetUserPostsCount(string username)
    {
        var posts = await postService.GetPostsByAuthorName(username);
        return Ok(posts.Count);
    }
        
    [HttpGet("/get-friends-posts/{username}")]
    public async Task<IActionResult> GetFriendsPosts(string username)
    {
        var friends = await friendshipService.GetFriendsUsernames(username); 
        return Ok(await postService.GetPostsByAuthorNames(friends));
    }
        
    [HttpGet("{postId:int}")]
    public async Task<IActionResult> GetPostById(int postId)
    {
        return Ok(await postService.GetPostById(postId));
    }
        
    [HttpGet("{postId:int}/image")]
    public async Task<IActionResult> GetPostImage(int postId)
    {
        var imageData = await postService.GetPostImage(postId);
            
        if (imageData is null)
        {
            return NotFound();
        }
            
        return File(imageData, "image/jpeg");
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDto request)
    {
        var newPost = await postService.CreatePost(request);
        return File(newPost.ImageData, "image/jpeg");
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequestDto request)
    {
        var result = await postService.UpdatePost(request);
        if (result is false)
        {
            return NotFound();
        }

        return Ok("Post updated.");
    }

    [HttpDelete("{postId:int}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        await postService.DeletePost(postId);
        return NoContent();
    }
}