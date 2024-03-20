using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class NewsRepository(UniVerseDbContext context) : INewsRepository
{
    public async Task<List<News>> GetNews()
    {
        return await context.News.ToListAsync();
    }

    public async Task<News?> GetNewsById(int id)
    {
        return await context.News.FindAsync(id);
    }

    public async Task<News?> GetNewsByTitle(string title)
    {
        return await context.News
            .FirstOrDefaultAsync(n => n.Title == title);
    }

    public async Task InsertNews(News news)
    {
        context.News.Add(news);
        await context.SaveChangesAsync();
    }

    public async Task UpdateNews(News news)
    {
        context.Entry(news).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteNews(int newsId)
    {
        var news = await context.News.FindAsync(newsId);
        
        if (news is null)
        {
            throw new NotFoundException();
        }
        
        context.News.Remove(news);
        await context.SaveChangesAsync();
    }
}