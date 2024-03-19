using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserByUsername(string username);
    Task<IEnumerable<User>> GetUsersByUsername(string username);
    Task<IEnumerable<User>> GetUsersByUsernames(List<string> usernames);
    Task<bool> ExistsByUsername(string username);
}