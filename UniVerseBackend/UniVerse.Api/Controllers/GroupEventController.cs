using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class GroupEventController(IGroupEventService groupEventService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllGroupEvents()
    {
        return Ok(await groupEventService.GetAllGroupEvents());
    }
    
    [HttpGet("trending")]
    public async Task<IActionResult> GetTrendingGroupEvents()
    {
        return Ok(await groupEventService.GetTrendingGroupEvents());
    }
    
    [HttpGet("{groupEventId:int}")]
    public async Task<IActionResult> GetGroupEventById(int groupEventId)
    {
        return Ok(await groupEventService.GetGroupEventById(groupEventId));
    }
    
    [HttpGet("{groupEventId:int}/is-attending/{username}")]
    public async Task<IActionResult> GetGroupEventIsAttending(int groupEventId, string username)
    {
        return Ok(await groupEventService.IsAttending(groupEventId, username));
    }
    
    [HttpPost("create-group-event")]
    public async Task<IActionResult> CreateGroupEvent([FromBody] CreateGroupEventRequestDto request)
    {
        await groupEventService.CreateGroupEvent(request);
        return Ok("Group event created.");
    }
    
    [HttpPost("{groupEventId:int}/attend/{username}")]
    public async Task<IActionResult> AttendEvent(int groupEventId, string username)
    {
        await groupEventService.AttendGroupEvent(groupEventId, username);
        return Ok("Attending group event.");
    }
    
    [HttpPost("{groupEventId:int}/remove-attending/{username}")]
    public async Task<IActionResult> RemoveAttending(int groupEventId, string username)
    {
        await groupEventService.RemoveAttending(groupEventId, username);
        return Ok("Attending group event removed.");
    }
    
    [HttpPut("{groupEventId:int}")]
    public async Task<IActionResult> UpdateGroupEvent(int groupEventId, [FromBody] UpdateGroupEventRequestDto request)
    {
        await groupEventService.UpdateGroupEvent(groupEventId, request);
        return Ok("Group event updated.");
    }
    
    [HttpDelete("{groupEventId:int}")]
    public async Task<IActionResult> DeleteGroupEvent(int groupEventId)
    {
        await groupEventService.DeleteGroupEvent(groupEventId);
        return Ok("Group event deleted.");
    }
}