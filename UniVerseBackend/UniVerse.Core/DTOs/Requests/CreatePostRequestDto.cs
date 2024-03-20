using Microsoft.AspNetCore.Http;

namespace UniVerse.Core.DTOs.Requests;

public record CreatePostRequestDto(
    string Title,
    string Content,
    string AuthorName,
    IFormFile Image);