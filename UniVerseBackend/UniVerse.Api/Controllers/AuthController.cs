using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IClients;
using UniVerse.Core.Interfaces.IServices;
using UniVerse.Infrastructure.Hubs;

namespace UniVerseBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IHubContext<ChatHub, IChatClient> hubContext,
    IAuthService authService,
    IUserService userService,
    ITokenService tokenService,
    IFriendshipService friendshipService) : ControllerBase
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
        if (await userService.ExistsByEmail(request.Email) is false)
        {
            return NotFound();
        }
        if (await authService.CheckIsEnabled(request.Email) is false)
        {
            return StatusCode(423, "Account not approved");
        }
        
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
            
        var friends = await friendshipService.GetFriendsUsernames(username);

        foreach (var friend in friends)
        {
            await hubContext.Clients.Group(friend).ReceiveOnlineAlert(username);
        }
        
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
        user.IsOnline = false;

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
        var role = user.Role.ToString().ToUpper();

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