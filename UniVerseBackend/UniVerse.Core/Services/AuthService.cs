using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Enums;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class AuthService(
    IUserRepository userRepository, 
    IPasswordHasher<User> passwordHasher,
    ITokenService tokenService) : IAuthService
{
    public async Task Register(RegisterRequestDto request)
    {
        if (await userRepository.GetUserByUsername(request.Username) is not null)
        {
            throw new UserAlreadyExistsException("Username already taken");
        }
        
        var user = new User
        {
            UserName = request.Username,
            Email = request.Email,
            Role = UserRole.User,
            IsEnabled = false,
            IsOnline = false
        };

        await userRepository.InsertUser(user, request.Password);
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto request)
    {
        var user = await userRepository.GetUserByEmail(request.Email);

        if (user is null || VerifyPassword(user, request.Password) is false)
        {
            throw new AuthenticationException("Incorrect email or password");
        }

        var accessToken = tokenService.CreateAccessToken(user);
        var refreshToken = await tokenService.CreateRefreshToken(user);

        user.IsOnline = true;

        await userRepository.UpdateUser(user);
        
        return new LoginResponseDto(accessToken, refreshToken, user.UserName, user.Role.ToString().ToUpper());
    }

    public async Task<bool> CheckPassword(string username, string password)
    {
        var user = await userRepository.GetUserByUsername(username);
        
        if (user?.PasswordHash is null)
        {
            return false;
        }
        
        return passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) is PasswordVerificationResult.Success;
    }

    public async Task<bool> CheckIsEnabled(string email)
    {
        var user = await userRepository.GetUserByEmail(email);
        
        if (user is null)
        {
            throw new NotFoundException();
        }

        return user.IsEnabled;
    }

    private bool VerifyPassword(User user, string password)
    {
        if (user.PasswordHash is null)
        {
            return false;
        }
        
        return passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) is PasswordVerificationResult.Success;
    }
}