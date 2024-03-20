using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IPostRepository
{
    Task<Post?> GetPostById(int id);
    Task<List<Post>> GetPosts();
    Task<List<Post>> GetPostsByAuthorNames(List<string> usernames);
    Task<List<Post>> GetPostsByAuthorName(string authorName);
    Task InsertPost(Post post);
    Task UpdatePost(Post post);
    Task DeletePost(int postId);
}