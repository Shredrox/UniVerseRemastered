using Microsoft.AspNetCore.Identity;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class UserService(
    IUserRepository userRepository,
    UserManager<User> userManager) : IUserService
{
    public async Task<List<User>> GetUsers()
    {
        return await userRepository.GetUsers();
    }

    public async Task<List<UserResponseDto>> GetUsersByFilter(string filter)
    {
        var users = await userRepository.GetUsersByUsername(filter);

        var response = users
            .Select(u => new UserResponseDto(u.UserName, u.Email))
            .ToList();

        return response;
    }

    public async Task<User?> GetUserById(string id)
    {
        return await userRepository.GetUserById(id);
    }
        
    public async Task<User?> GetUserByName(string name)
    {
        return await userRepository.GetUserByUsername(name);
    }

    public async Task<User?> GetUserFromRefreshToken(string refreshToken)
    {
        var user = await userRepository.GetUserByRefreshToken(refreshToken);

        if (user is null)
        {
            return null;
        }
        
        return await userRepository.GetUserById(user.Id);
    }

    public async Task<byte[]?> GetUserProfilePicture(string username)
    {
        var user = await userRepository.GetUserByUsername(username);
        return user?.ProfilePicture;
    }

    public async Task<bool> ExistsByUsername(string username)
    {
        return await userRepository.ExistsByUsername(username);
    }

    public async Task UpdateUserRefreshToken(User user)
    {
        var existingUser = await userRepository.GetUserById(user.Id);

        if(existingUser == null)
        {
            return;
        }

        existingUser.RefreshToken = user.RefreshToken;
        existingUser.RefreshTokenValidity = user.RefreshTokenValidity;

        await userRepository.UpdateUser(user);
    }

    public async Task<bool> UpdateUserProfile(UpdateProfileRequestDto request)
    {
        var user = await userRepository.GetUserByUsername(request.Username);

        if (user == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(request.NewUsername))
        {
            user.UserName = request.NewUsername;
        }
        if (!string.IsNullOrEmpty(request.NewEmail))
        {
            user.Email = request.NewEmail;
        }
        if (!string.IsNullOrEmpty(request.NewPassword))
        {
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, request.NewPassword);
        }
        if (request.ProfilePicture != null)
        {
            using var memoryStream = new MemoryStream();
            await request.ProfilePicture.CopyToAsync(memoryStream);
            user.ProfilePicture = memoryStream.ToArray();
        }

        await userRepository.UpdateUser(user);

        return true;
    }
}