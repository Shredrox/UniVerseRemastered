using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController(
    IUserService userService,
    IAuthService authService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await userService.GetUsers());
    }
        
    [HttpGet("search/{filter}")]
    public async Task<IActionResult> GetUsersByFilter(string filter)
    {
        return Ok(await userService.GetUsersByFilter(filter));
    }
        
    [HttpGet("exists/{username}")]
    public async Task<IActionResult> UserExists(string username)
    {
        return Ok(await userService.ExistsByUsername(username));
    }
    
    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserByName(string username)
    {
        var user = await userService.GetUserByName(username);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(new UserResponseDto(user.UserName, user.Email));
    }
        
    [HttpGet("{username}/profile-picture")]
    public async Task<IActionResult> GetUserProfilePicture(string username)
    {
        var imageBytes = await userService.GetUserProfilePicture(username);

        if (imageBytes is null)
        {
            return NotFound();
        }
            
        return File(imageBytes, "image/jpeg");
    }
        
    [HttpPost("update-profile")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateProfileRequestDto request)
    {
        var result = await userService.UpdateUserProfile(request);

        if (!result)
        {
            return BadRequest();
        }

        return Ok("Profile updated.");
    }
}