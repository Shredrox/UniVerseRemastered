namespace UniVerse.Core.DTOs.Requests;

public record RegisterRequestDto(
    string Username,
    string Email,
    string Password);