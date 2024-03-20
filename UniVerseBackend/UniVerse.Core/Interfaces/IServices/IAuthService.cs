using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;

namespace UniVerse.Core.Interfaces.IServices;

public interface IAuthService
{
    Task Register(RegisterRequestDto request);
    Task<LoginResponseDto> Login(LoginRequestDto request);
    Task<bool> CheckPassword(string username, string password);
}