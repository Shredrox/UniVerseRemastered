using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class LikeRepository(UniVerseDbContext context) : ILikeRepository
{
    public async Task<List<Like>> GetLikesByPostId(int postId)
    {
        return await context.Likes
            .Where(l => l.PostId == postId)
            .ToListAsync();
    }

    public async Task<List<Like>> GetLikesByPostIdAndUserId(int postId, string userId)
    {
        return await context.Likes
            .Where(l => l.PostId == postId && l.UserId == userId)
            .ToListAsync();
    }

    public Task<bool> ExistsByPostIdAndUserId(int postId, string userId)
    {
        return context.Likes
            .AnyAsync(l => l.PostId == postId && l.UserId == userId);
    }

    public async Task InsertLike(Like like)
    {
        context.Likes.Add(like);
        await context.SaveChangesAsync();
    }

    public async Task DeleteLikesByPostId(int postId)
    {
        var likes = await context.Likes
            .Where(l => l.PostId == postId)
            .ToListAsync();
        
        if (likes is null)
        {
            throw new NotFoundException();
        }
        
        context.Likes.RemoveRange(likes);
        await context.SaveChangesAsync();
    }

    public async Task DeleteLikeByPostIdAndUserId(int postId, string userId)
    {
        var like = await context.Likes
            .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
        
        if (like is null)
        {
            throw new NotFoundException();
        }
        
        context.Likes.Remove(like);
        await context.SaveChangesAsync();
    }
}