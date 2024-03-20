namespace UniVerse.Core.DTOs.Requests;

public record CreateGroupEventRequestDto(
    string Title,
    string Description,
    string OrganiserName,
    DateTime Date);