using Microsoft.AspNetCore.Http;

namespace UniVerse.Core.DTOs.Requests;

public record UpdateProfileRequestDto(
    string Username,
    string NewUsername,
    string NewEmail,
    string NewPassword,
    IFormFile? ProfilePicture);