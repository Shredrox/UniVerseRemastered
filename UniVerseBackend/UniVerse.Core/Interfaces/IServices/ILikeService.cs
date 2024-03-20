namespace UniVerse.Core.Interfaces.IServices;

public interface ILikeService
{
    Task<int> GetPostLikes(int postId);
    Task<bool> IsPostLiked(int postId, string username);
    Task LikePost(int postId, string username);
    Task UnlikePost(int postId, string username);
}