namespace UniVerse.Core.DTOs.Responses;

public record LoginResponseDto(
    string AccessToken,
    string RefreshToken,
    string Username,
    string Role);