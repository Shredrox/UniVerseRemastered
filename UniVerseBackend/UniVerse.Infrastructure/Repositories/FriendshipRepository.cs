using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Enums;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class FriendshipRepository(UniVerseDbContext context) : IFriendshipRepository
{
    public async Task<Friendship?> GetFriendshipById(int friendshipId)
    {
        return await context.Friendships.FindAsync(friendshipId);
    }

    public async Task<Friendship?> GetFriendshipByUsers(User user1, User user2)
    {
        return await context.Friendships
            .FirstOrDefaultAsync(f => (f.User1 == user1 && f.User2 == user2) || (f.User1 == user2 && f.User2 == user1));
    
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

    public async Task InsertFriendship(Friendship friendship)
    {
        context.Friendships.Add(friendship);
        await context.SaveChangesAsync();
    }

    public async Task UpdateFriendship(Friendship friendship)
    {
        context.Entry(friendship).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteFriendship(int friendshipId)
    {
        var friendship = await context.Friendships.FindAsync(friendshipId);

        if (friendship is null)
        {
            throw new NotFoundException();
        }
        
        context.Friendships.Remove(friendship);
        await context.SaveChangesAsync();
    }

    public async Task DeleteFriendshipByUsers(User user1, User user2)
    {
        var friendship = await context.Friendships
            .FirstOrDefaultAsync(f => (f.User1 == user1 && f.User2 == user2) || (f.User1 == user2 && f.User2 == user1));
        
        if (friendship is null)
        {
            throw new NotFoundException();
        }
        
        context.Friendships.Remove(friendship);
        await context.SaveChangesAsync();
    }
}