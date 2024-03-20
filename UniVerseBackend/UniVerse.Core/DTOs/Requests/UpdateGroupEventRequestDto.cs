namespace UniVerse.Core.DTOs.Requests;

public record UpdateGroupEventRequestDto(
    string Title,
    string Description,
    DateTime Date);