namespace UniVerse.Core.DTOs.Responses;

public record GroupEventResponseDto(
    int Id,
    string Title,
    string Description,
    string Date);