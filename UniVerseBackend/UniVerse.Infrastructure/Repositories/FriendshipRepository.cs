using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Enums;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class FriendshipRepository(UniVerseDbContext context) : IFriendshipRepository
{
    public Task<Friendship> GetFriendshipByUsers(User user1, User user2)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Friendship>> GetFriendshipsByUserAndStatus(User user, FriendshipStatus status)
    {
        return await context.Friendships
            .Where(f => f.User2 == user && f.FriendshipStatus == status)
            .ToListAsync();
    }

    public async Task<List<Friendship>> GetFriendshipsByUser1OrUser2(User user1, User user2)
    {
        return await context.Friendships
            .Where(f => f.User1 == user1 || f.User2 == user2)
            .ToListAsync();
    }

    public Task DeleteFriendshipByUsers(User user1, User user2)
    {
        throw new NotImplementedException();
    }
}