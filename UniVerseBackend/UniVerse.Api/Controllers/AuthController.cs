using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IAuthService authService,
    IUserService userService,
    ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        await authService.Register(request);
        return Ok("User Registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var (accessToken, refreshToken, username, role) = await authService.Login(request);

        Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(15),
            Domain = "localhost",
            IsEssential = true,
            SameSite = SameSiteMode.None
        });
            
        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddHours(1),
            Domain = "localhost",
            IsEssential = true,
            SameSite = SameSiteMode.None
        });
            
        return Ok(new { username, accessToken, role });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["RefreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token is required");
        }

        var user = await userService.GetUserFromRefreshToken(refreshToken);

        if (user is null)
        {
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");
            return NoContent();
        }

        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");
            
        user.RefreshToken = null;
        user.RefreshTokenValidity = null;

        await userService.UpdateUserRefreshToken(user);

        return NoContent();
    }

    [HttpPost("confirm-password")]
    public async Task<IActionResult> ConfirmPassword([FromBody] RegisterRequestDto request)
    {
        return Ok(await authService.CheckPassword(request.Username, request.Password));
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["RefreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token is required");
        }

        var user = await userService.GetUserFromRefreshToken(refreshToken);

        if (user is null)
        {
            return BadRequest("Invalid refresh token");
        }

        var username = user.UserName;
        var newAccessToken = tokenService.CreateAccessToken(user);
        var newRefreshToken = await tokenService.CreateRefreshToken(user);
        var role = user.Role;

        Response.Cookies.Append("AccessToken", newAccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(15),
            Domain = "localhost",
            IsEssential = true,
            SameSite = SameSiteMode.None
        });
            
        Response.Cookies.Append("RefreshToken", newRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddHours(1),
            Domain = "localhost",
            IsEssential = true,
            SameSite = SameSiteMode.None
        });
            
        return Ok(new { newAccessToken, username, role });
    }
}