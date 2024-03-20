using Microsoft.AspNetCore.Http;

namespace UniVerse.Core.DTOs.Requests;

public record CreateNewsRequestDto(
    string Title,
    string Content,
    bool Pinned,
    IFormFile Image);