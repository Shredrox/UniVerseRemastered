namespace UniVerse.Core.DTOs.Requests;

public record LoginRequestDto(
    string Email,
    string Password);