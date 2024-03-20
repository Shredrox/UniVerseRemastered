using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class PostService(
    IPostRepository postRepository,
    IUserRepository userRepository) : IPostService
{
    public async Task<Post?> GetPostById(int id)
    {
        return await postRepository.GetPostById(id);
    }

    public async Task<List<PostResponseDto>> GetAllPosts()
    {
        var posts = await postRepository.GetPosts();

        var response = posts
            .Select(p => new PostResponseDto(
                p.Id, 
                p.Title, 
                p.Content, 
                p.ImageData, 
                p.AuthorName, 
                p.Timestamp)
            )
            .ToList();

        return response;
    }

    public async Task<List<PostResponseDto>> GetPostsByAuthorName(string authorName)
    {
        var posts = await postRepository.GetPostsByAuthorName(authorName);

        var response = posts
            .Select(p => new PostResponseDto(
                p.Id, 
                p.Title, 
                p.Content, 
                p.ImageData, 
                p.AuthorName, 
                p.Timestamp)
            )
            .ToList();

        return response;
    }

    public async Task<List<PostResponseDto>> GetPostsByAuthorNames(List<string> authorNames)
    {
        var posts= await postRepository.GetPostsByAuthorNames(authorNames);
        
        var response = posts
            .Select(p => new PostResponseDto(
                p.Id, 
                p.Title, 
                p.Content, 
                p.ImageData, 
                p.AuthorName, 
                p.Timestamp)
            )
            .ToList();

        return response;
    }

    public async Task<byte[]?> GetPostImage(int id)
    {
        var post = await postRepository.GetPostById(id);
        return post?.ImageData;
    }

    public async Task<Post> CreatePost(CreatePostRequestDto request)
    {
        var user = await userRepository.GetUserByUsername(request.AuthorName);
        
        using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream);

        var post = new Post
        {
            Title = request.Title,
            Content = request.Content,
            AuthorName = request.AuthorName,
            ImageData = memoryStream.ToArray(),
            Timestamp = DateTime.Now,
            User = user
        };

        await postRepository.InsertPost(post);

        return post;
    }

    public async Task<bool> UpdatePost(UpdatePostRequestDto request)
    {
        var post = await postRepository.GetPostById(request.PostId);

        if (post is null)
        {
            return false;
        }
        
        post.Content = request.Content;
        await postRepository.UpdatePost(post);

        return true;
    }

    public async Task DeletePost(int postId)
    {
        await postRepository.DeletePost(postId);
    }
}