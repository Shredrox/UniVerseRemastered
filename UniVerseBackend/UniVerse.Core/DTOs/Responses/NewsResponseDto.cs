namespace UniVerse.Core.DTOs.Responses;

public record NewsResponseDto(
    int Id,
    string Title,
    string Content,
    byte[] Image,
    bool Pinned,
    DateTime Date);