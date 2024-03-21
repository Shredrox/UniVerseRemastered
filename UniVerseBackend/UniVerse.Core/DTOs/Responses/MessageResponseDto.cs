namespace UniVerse.Core.DTOs.Responses;

public record MessageResponseDto(
    string Content,
    string Sender,
    string Receiver,
    string Timestamp);