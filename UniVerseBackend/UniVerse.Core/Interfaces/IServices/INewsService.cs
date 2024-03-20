using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;

namespace UniVerse.Core.Interfaces.IServices;

public interface INewsService
{
    Task<List<NewsResponseDto>> GetAllNews();
    Task<NewsResponseDto> GetNewsById(int newsId);
    Task<byte[]?> GetNewsImage(int newsId);
    Task CreateNews(CreateNewsRequestDto request);
    Task UpdateNews(NewsEditRequestDto request);
    Task DeleteNews(int newsId);
}