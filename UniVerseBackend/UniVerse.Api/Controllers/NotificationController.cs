using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IClients;
using UniVerse.Core.Interfaces.IServices;
using UniVerse.Infrastructure.Hubs;

namespace UniVerseBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController(
    IHubContext<ChatHub, IChatClient> hubContext,
    INotificationService notificationService) : ControllerBase
{
    [HttpGet("{username}")]
    public async Task<IActionResult> GetNotifications(string username)
    {
        return Ok(await notificationService.GetUserNotifications(username));
    }
    
    [HttpPost("send-notification")]
    public async Task<IActionResult> SendNotification([FromBody] AddNotificationRequestDto request)
    {
        var notificationResponse = await notificationService.CreateNotification(request);
        
        await hubContext.Clients.Group(request.RecipientName).ReceiveNotification(notificationResponse);
        
        return Ok();
    }
    
    [HttpPost("{username}/set-read")]
    public async Task<IActionResult> ReadNotifications(string username)
    {
        await notificationService.ReadNotifications(username);
        return Ok();
    }
}