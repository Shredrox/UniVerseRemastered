using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class LikeService(
    ILikeRepository likeRepository,
    IUserRepository userRepository,
    IPostRepository postRepository) : ILikeService
{
    public async Task<int> GetPostLikes(int postId)
    {
        var likes = await likeRepository.GetLikesByPostId(postId);
        return likes.Count;
    }

    public async Task<bool> IsPostLiked(int postId, string username)
    {
        var user = await userRepository.GetUserByUsername(username);

        if (user is null)
        {
            throw new NotFoundException();
        }
        
        return await likeRepository.ExistsByPostIdAndUserId(postId, user.Id);
    }

    public async Task LikePost(int postId, string username)
    {
        var user = await userRepository.GetUserByUsername(username);
        var post = await postRepository.GetPostById(postId);
        
        if (user is null || post is null)
        {
            throw new NotFoundException();
        }

        var likes = await likeRepository.GetLikesByPostIdAndUserId(postId, user.Id);
        if (likes.Count > 0)
        {
            return;
        }

        var like = new Like
        {
            User = user,
            Post = post,
        };

        await likeRepository.InsertLike(like);
    }

    public async Task UnlikePost(int postId, string username)
    {
        var user = await userRepository.GetUserByUsername(username);
        if (user is null)
        {
            throw new NotFoundException();
        }
        
        await likeRepository.DeleteLikeByPostIdAndUserId(postId, user.Id);
    }
}