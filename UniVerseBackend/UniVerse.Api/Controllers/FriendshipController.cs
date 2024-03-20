using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FriendshipController(IFriendshipService friendshipService) : ControllerBase
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
    public async Task<IActionResult> CheckFriendship([FromQuery] string user1name, [FromQuery] string user2Name)
    {
        return Ok(await friendshipService.CheckUsersFriendship(user1name, user2Name));
    }
        
    //TODO: Implement socket/signalR endpoints for friend requests
        
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