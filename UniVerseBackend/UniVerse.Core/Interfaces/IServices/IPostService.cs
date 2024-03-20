using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IServices;

public interface IPostService
{
    Task<Post?> GetPostById(int id);
    Task<List<PostResponseDto>> GetAllPosts();
    Task<List<PostResponseDto>> GetPostsByAuthorName(string authorName);
    Task<List<PostResponseDto>> GetPostsByAuthorNames(List<string> authorNames);
    Task<byte[]?> GetPostImage(int id);
    Task<Post> CreatePost(CreatePostRequestDto request);
    Task<bool> UpdatePost(UpdatePostRequestDto request);
    Task DeletePost(int postId);
}