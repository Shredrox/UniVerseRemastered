namespace UniVerse.Core.DTOs.Requests;

public record AddNotificationRequestDto(
    string Message,
    string Type,
    string Source,
    string RecipientName);