using UniVerse.Core.Entities;
using UniVerse.Core.Enums;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IFriendshipRepository
{
    Task<Friendship?> GetFriendshipById(int friendshipId);
    Task<Friendship?> GetFriendshipByUsers(User user1, User user2);
    Task<List<Friendship>> GetFriendshipsByUserAndStatus(User user, FriendshipStatus status);
    Task<List<Friendship>> GetFriendshipsByUser1OrUser2(User user1, User user2);
    Task InsertFriendship(Friendship friendship);
    Task UpdateFriendship(Friendship friendship);
    Task DeleteFriendship(int friendshipId);
    Task DeleteFriendshipByUsers(User user1, User user2);
}