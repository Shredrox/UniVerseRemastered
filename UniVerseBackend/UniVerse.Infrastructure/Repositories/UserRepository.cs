using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class UserRepository(
    UserManager<User> userManager,
    UniVerseDbContext context) : IUserRepository
{
    public async Task<User?> GetUserById(string id)
    {
        return await userManager.FindByIdAsync(id);
    }
    
    public async Task<List<User>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }
    
    public async Task<User?> GetUserByEmail(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await userManager.FindByNameAsync(username);
    }

    public async Task<List<User>> GetUsersByUsername(string username)
    {
        return await context.Users
            .Where(u => u.UserName == username)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersByUsernames(List<string> usernames)
    {
        return await context.Users
            .Where(p => usernames.Contains(p.UserName))
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersByEnabled(bool enabled)
    {
        return await context.Users
            .Where(u => u.IsEnabled == enabled)
            .ToListAsync();
    }

    public async Task<User?> GetUserByRefreshToken(string refreshToken)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenValidity > DateTime.Now.ToUniversalTime());
    }

    public async Task<bool> ExistsByUsername(string username)
    {
        return await context.Users.AnyAsync(u => u.UserName == username);
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task InsertUser(User user, string password)
    {
        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, password);
        await userManager.CreateAsync(user, password);
    }

    public async Task UpdateUser(User user)
    {
        await userManager.UpdateAsync(user);
    }

    public async Task DeleteUser(string userId)
    {
        var user = await context.Users.FindAsync(userId);
        
        if (user is null)
        {
            throw new NotFoundException();
        }
        
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}