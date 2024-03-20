namespace UniVerse.Core.DTOs.Requests;

public record UpdatePostRequestDto(
    int PostId,
    string Content);