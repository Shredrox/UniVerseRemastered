using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Enums;

namespace UniVerse.Core.Interfaces.IServices;

public interface IFriendshipService
{
    Task<List<FriendRequestResponseDto>> GetFriendRequests(string username);
    Task<List<string>> GetFriendsUsernames(string username);
    Task<List<User>> GetOnlineFriends(string username);
    Task<FriendshipStatus> CheckUsersFriendship(string user1Name, string user2Name);
    Task<FriendRequestResponseDto> CreateFriendship(FriendRequestDto request);
    Task AcceptFriendRequest(int friendshipId);
    Task RejectFriendRequest(int friendshipId);
    Task DeleteFriendship(string user1Name, string user2Name);
}