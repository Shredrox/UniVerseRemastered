using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Enums;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class FriendshipService(
    IFriendshipRepository friendshipRepository,
    IUserRepository userRepository) : IFriendshipService
{
    public async Task<List<FriendRequestResponseDto>> GetFriendRequests(string username)
    {
        var user = await userRepository.GetUserByUsername(username);

        if (user is null)
        {
            throw new NotFoundException();
        }
        
        var friendships = await friendshipRepository.GetFriendshipsByUserAndStatus(user, FriendshipStatus.Pending);

        var response = friendships
            .Select(f => new FriendRequestResponseDto(f.Id, f.User1.UserName))
            .ToList();

        return response;
    }

    public async Task<List<string>> GetFriendsUsernames(string username)
    {
        var user = await userRepository.GetUserByUsername(username);
        
        if (user is null)
        {
            throw new NotFoundException();
        }

        var friendships = await friendshipRepository.GetFriendshipsByUser1OrUser2(user, user);

        var response = friendships
            .Where(f => f.FriendshipStatus == FriendshipStatus.Accepted)
            .Select(friendship => friendship.User1 == user ? friendship.User2.UserName : friendship.User1.UserName)
            .ToList();

        return response;
    }

    public async Task<List<User>> GetOnlineFriends(string username)
    {
        var user = await userRepository.GetUserByUsername(username);
        var friendsUsernames = await GetFriendsUsernames(username);
        var friends = await userRepository.GetUsersByUsernames(friendsUsernames);

        return friends
            .Where(f => f.IsOnline)
            .ToList();
    }

    public async Task<FriendshipStatus> CheckUsersFriendship(string user1Name, string user2Name)
    {
        var user1 = await userRepository.GetUserByUsername(user1Name);
        var user2 = await userRepository.GetUserByUsername(user2Name);

        if (user1 is null || user2 is null)
        {
            throw new NotFoundException();
        }

        var friendship = await friendshipRepository.GetFriendshipByUsers(user1, user2);

        return friendship?.FriendshipStatus ?? FriendshipStatus.NotFriends;
    }

    public async Task CreateFriendship(string senderName, string receiverName)
    {
        var sender = await userRepository.GetUserByUsername(senderName);
        var receiver = await userRepository.GetUserByUsername(receiverName);

        if (sender is null || receiver is null)
        {
            throw new NotFoundException();
        }
        
        var friendship = new Friendship
        {
            User1 = sender,
            User2 = receiver,
            FriendshipStatus = FriendshipStatus.Pending,
            Date = DateTime.Now
        };

        await friendshipRepository.InsertFriendship(friendship);
    }

    public async Task AcceptFriendRequest(int friendshipId)
    {
        var friendship = await friendshipRepository.GetFriendshipById(friendshipId);

        if (friendship is null)
        {
            throw new NotFoundException();
        }
        
        friendship.FriendshipStatus = FriendshipStatus.Accepted;

        await friendshipRepository.UpdateFriendship(friendship);
    }

    public async Task RejectFriendRequest(int friendshipId)
    {
        await friendshipRepository.DeleteFriendship(friendshipId);
    }

    public async Task DeleteFriendship(string user1Name, string user2Name)
    {
        var user1 = await userRepository.GetUserByUsername(user1Name);
        var user2 = await userRepository.GetUserByUsername(user2Name);

        if (user1 is null || user2 is null)
        {
            throw new NotFoundException();
        }

        await friendshipRepository.DeleteFriendshipByUsers(user1, user2);
    }
}