using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface INewsRepository
{
    Task<List<News>> GetNews();
    Task<News?> GetNewsById(int id);
    Task<News?> GetNewsByTitle(string title);
    Task InsertNews(News news);
    Task UpdateNews(News news);
    Task DeleteNews(int newsId);
}