using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class NewsController(INewsService newsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllNews()
    {
        return Ok(await newsService.GetAllNews());
    }
    
    [HttpGet("{newsId:int}")]
    public async Task<IActionResult> GetNewsById(int newsId)
    {
        return Ok(await newsService.GetNewsById(newsId));
    }
    
    [HttpGet("{newsId:int}/image")]
    public async Task<IActionResult> GetNewsImage(int newsId)
    {
        var imageData = await newsService.GetNewsImage(newsId);
            
        if (imageData is null)
        {
            return NotFound();
        }
            
        return File(imageData, "image/jpeg");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("create-news")]
    public async Task<IActionResult> CreateNews([FromForm] CreateNewsRequestDto request)
    {
        await newsService.CreateNews(request);
        return Ok("News created.");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateNews([FromForm] NewsEditRequestDto request)
    {
        await newsService.UpdateNews(request);
        return Ok("News updated.");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{newsId:int}")]
    public async Task<IActionResult> DeleteNews(int newsId)
    {
        await newsService.DeleteNews(newsId);
        return Ok("News deleted.");
    }
}