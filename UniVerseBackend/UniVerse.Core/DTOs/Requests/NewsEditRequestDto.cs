using Microsoft.AspNetCore.Http;

namespace UniVerse.Core.DTOs.Requests;

public record NewsEditRequestDto(
    int NewsId,
    string Title,
    string Content,
    bool Pinned,
    IFormFile? Image);