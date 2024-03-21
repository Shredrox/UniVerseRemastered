using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IClients;
using UniVerse.Core.Interfaces.IServices;
using UniVerse.Infrastructure.Hubs;

namespace UniVerseBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController(
    IHubContext<ChatHub, IChatClient> hubContext,
    IChatService chatService) : ControllerBase
{
    [HttpGet("get-messages")]
    public async Task<IActionResult> GetMessages([FromQuery] string user, [FromQuery] string chatUser)
    {
        return Ok(await chatService.GetChatMessages(user, chatUser));
    }
    
    [HttpGet("get-user-chats")]
    public async Task<IActionResult> GetUserChats([FromQuery] string username)
    {
        return Ok(await chatService.GetUserChats(username));
    }
    
    [HttpPost("create-chat")]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatRequestDto request)
    {
        var chatResponse = await chatService.CreateChat(request);
        
        await hubContext.Clients.Group(request.User2Name).ChatCreated(chatResponse);
        
        return Ok();
    }
    
    [HttpPost("send-private-message")]
    public async Task<IActionResult> SendPrivateMessage([FromBody] SendMessageRequestDto request)
    {
        var messageResponse = await chatService.CreateMessage(request);
        
        await hubContext.Clients.Group(request.Receiver).ReceiveMessage(messageResponse);
        
        return Ok();
    }
}