using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class PostRepository(UniVerseDbContext context) : IPostRepository
{
    public async Task<Post?> GetPostById(int id)
    {
        return await context.Posts.FindAsync(id);
    }

    public async Task<List<Post>> GetPosts()
    {
        return await context.Posts.ToListAsync();
    }

    public async Task<List<Post>> GetPostsByAuthorNames(List<string> usernames)
    {
        return await context.Posts
            .Where(p => usernames.Contains(p.AuthorName))
            .ToListAsync();
    }

    public async Task<List<Post>> GetPostsByAuthorName(string authorName)
    {
        return await context.Posts
            .Where(p => p.AuthorName == authorName)
            .ToListAsync();
    }

    public async Task InsertPost(Post post)
    {
        context.Posts.Add(post);
        await context.SaveChangesAsync();
    }

    public async Task UpdatePost(Post post)
    {
        context.Entry(post).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeletePost(int postId)
    {
        var post = await context.Posts.FindAsync(postId);
        
        if (post is null)
        {
            throw new NotFoundException();
        }
        
        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }
}