using Microsoft.AspNetCore.Http;

namespace UniVerse.Core.DTOs.Requests;

public class NewsEditRequestDto(
    int Id,
    string Title,
    string Content,
    bool Pinned,
    IFormFile? Image);