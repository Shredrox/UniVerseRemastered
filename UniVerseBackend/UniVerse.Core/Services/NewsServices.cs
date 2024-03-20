using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class NewsServices(INewsRepository newsRepository) : INewsService
{
    public async Task<List<NewsResponseDto>> GetAllNews()
    {
        var news = await newsRepository.GetNews();

        var response = news
            .Select(n => new NewsResponseDto(n.Id, n.Title, n.Content, n.ImageData, n.Pinned, n.Date))
            .ToList();

        return response;
    }

    public async Task<NewsResponseDto> GetNewsById(int newsId)
    {
        var news = await newsRepository.GetNewsById(newsId);
        if (news is null)
        {
            throw new NotFoundException();
        }
        
        return new NewsResponseDto(news.Id, news.Title, news.Content, news.ImageData, news.Pinned, news.Date);
    }

    public async Task<byte[]?> GetNewsImage(int newsId)
    {
        var news = await newsRepository.GetNewsById(newsId);
        return news?.ImageData;
    }

    public async Task CreateNews(CreateNewsRequestDto request)
    {
        using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream);

        var news = new News
        {
            Title = request.Title,
            Content = request.Content,
            Pinned = request.Pinned,
            ImageData = memoryStream.ToArray(),
            Date = DateTime.Now
        };

        await newsRepository.InsertNews(news);
    }

    public async Task UpdateNews(NewsEditRequestDto request)
    {
        using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream);

        var news = await newsRepository.GetNewsById(request.NewsId);

        if (news is null)
        {
            throw new NotFoundException();
        }
        
        news.Title = request.Title;
        news.Content = request.Content;
        news.Pinned = request.Pinned;
        news.ImageData = memoryStream.ToArray();

        await newsRepository.UpdateNews(news);
    }

    public async Task DeleteNews(int newsId)
    {
        await newsRepository.DeleteNews(newsId);
    }
}