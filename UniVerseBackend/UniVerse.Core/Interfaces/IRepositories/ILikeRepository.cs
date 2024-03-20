using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface ILikeRepository
{
    Task<List<Like>> GetLikesByPostId(int postId);
    Task<List<Like>> GetLikesByPostIdAndUserId(int postId, string userId);
    Task<bool> ExistsByPostIdAndUserId(int postId, string userId);
    Task InsertLike(Like like);
    Task DeleteLikesByPostId(int postId);
    Task DeleteLikeByPostIdAndUserId(int postId, string userId);
}