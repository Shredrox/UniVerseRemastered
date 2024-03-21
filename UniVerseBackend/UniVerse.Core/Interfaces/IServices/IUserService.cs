using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IServices;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<List<UserResponseDto>> GetUsersByFilter(string filter);
    Task<User?> GetUserById(string id);
    Task<User?> GetUserByName(string name);
    Task<User?> GetUserFromRefreshToken(string refreshToken);
    Task<byte[]?> GetUserProfilePicture(string username);
    Task<bool> ExistsByUsername(string username);
    Task<bool> ExistsByEmail(string email);
    Task UpdateUserRefreshToken(User user);
    Task<bool> UpdateUserProfile(UpdateProfileRequestDto request);
    Task<List<UserResponseDto>> GetUserRegistrationRequests();
    Task ApproveUser(string username);
    Task RejectUser(string username);
}