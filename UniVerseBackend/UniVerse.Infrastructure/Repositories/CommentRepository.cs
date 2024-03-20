using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class CommentRepository(UniVerseDbContext context) : ICommentRepository
{
    public async Task<List<Comment>> GetComments()
    {
        return await context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetCommentsById(int id)
    {
        return await context.Comments.FindAsync(id);
    }

    public async Task<List<Comment>> GetCommentsByPostId(int postId)
    {
        return await context.Comments
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsByPostIdAndParentCommentIsNull(int postId)
    {
        return await context.Comments
            .Where(c => c.PostId == postId && c.ParentComment == null)
            .ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsByParentCommentId(int parentCommentId)
    {
        return await context.Comments
            .Where(c => c.ParentCommentId == parentCommentId)
            .ToListAsync();
    }

    public async Task InsertComment(Comment comment)
    {
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
    }

    public async Task UpdateComment(Comment comment)
    {
        context.Entry(comment).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteComment(int commentId)
    {
        var comment = await context.Comments.FindAsync(commentId);
        
        if (comment is null)
        {
            throw new NotFoundException();
        }
        
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCommentsByPostId(int postId)
    {
        var comments = await context.Comments
            .Where(c => c.PostId == postId)
            .ToListAsync();
        
        if (comments is null)
        {
            throw new NotFoundException();
        }
        
        context.Comments.RemoveRange(comments);
        await context.SaveChangesAsync();
    }
}