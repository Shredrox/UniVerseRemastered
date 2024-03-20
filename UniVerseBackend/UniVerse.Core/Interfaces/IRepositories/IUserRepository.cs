using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserById(string id);
    Task<List<User>> GetUsers();
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByUsername(string username);
    Task<List<User>> GetUsersByUsername(string username);
    Task<List<User>> GetUsersByUsernames(List<string> usernames);
    Task<User?> GetUserByRefreshToken(string refreshToken);
    Task<bool> ExistsByUsername(string username);
    Task InsertUser(User user, string password);
    Task UpdateUser(User user);
}