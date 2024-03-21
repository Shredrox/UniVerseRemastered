namespace UniVerse.Core.DTOs.Responses;

public record PostResponseDto(
    int Id,
    string Title,
    string Content,
    byte[] ImageData,
    string AuthorName,
    string Timestamp);