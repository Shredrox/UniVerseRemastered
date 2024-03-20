using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IServices;

public interface ITokenService
{
    string CreateAccessToken(User user);
    Task<string> CreateRefreshToken(User user);
}