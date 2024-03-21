using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IClients;
using UniVerse.Core.Interfaces.IServices;
using UniVerse.Infrastructure.Hubs;

namespace UniVerseBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FriendshipController(
    IHubContext<ChatHub, IChatClient> hubContext,
    IFriendshipService friendshipService) : ControllerBase
{
    [HttpGet("{username}/online-friends")]
    public async Task<IActionResult> GetUserOnlineFriends(string username)
    {
        var onlineFriends = await friendshipService.GetOnlineFriends(username);
        var onlineFriendsUsernames = onlineFriends
            .Select(u => u.UserName)
            .ToList(); 
            
        return Ok(onlineFriendsUsernames);
    }

    [HttpGet("{username}/count")]
    public async Task<IActionResult> GetUserFriendsCount(string username)
    {
        var friends = await friendshipService.GetFriendsUsernames(username);
        return Ok(friends.Count);
    }
        
    [HttpGet("{username}/friend-requests")]
    public async Task<IActionResult> GetUserFriendRequests(string username)
    {
        return Ok(await friendshipService.GetFriendRequests(username));
    }

    [HttpGet("check-friendship")]
    public async Task<IActionResult> CheckFriendship([FromQuery] string user1Name, [FromQuery] string user2Name)
    {
        var friendshipStatus = await friendshipService.CheckUsersFriendship(user1Name, user2Name);
        return Ok(friendshipStatus.ToString().ToUpper());
    }
        
    [HttpPost("send-friend-request")]
    public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestDto request)
    {
        var friendRequestResponse = await friendshipService.CreateFriendship(request);
        
        await hubContext.Clients.Group(request.Receiver).ReceiveFriendRequest(friendRequestResponse);
        
        return Ok();
    }
    
    [HttpPost("send-online-alert")]
    public async Task<IActionResult> SendOnlineAlert([FromQuery] string username)
    {
        var friends = await friendshipService.GetFriendsUsernames(username);

        foreach (var friend in friends)
        {
            await hubContext.Clients.Group(friend).ReceiveOnlineAlert(username);
        }
        
        return Ok();
    }
        
    [HttpPost("accept-friend-request/{friendRequestId:int}")]
    public async Task<IActionResult> AcceptFriendRequest(int friendRequestId)
    {
        await friendshipService.AcceptFriendRequest(friendRequestId);
        return Ok("Friend request accepted.");
    }
        
    [HttpDelete("reject-friend-request/{friendRequestId:int}")]
    public async Task<IActionResult> RejectFriendRequest(int friendRequestId)
    {
        await friendshipService.RejectFriendRequest(friendRequestId);
        return Ok("Friend request rejected.");
    }
        
    [HttpDelete("{user1Name}/remove-friend/{user2Name}")]
    public async Task<IActionResult> RemoveFriend(string user1Name, string user2Name)
    {
        await friendshipService.DeleteFriendship(user1Name, user2Name);
        return Ok("Friend removed.");
    }
}