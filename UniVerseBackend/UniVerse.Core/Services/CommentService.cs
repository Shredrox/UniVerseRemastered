using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class CommentService(
    ICommentRepository commentRepository,
    IPostRepository postRepository,
    IUserRepository userRepository) : ICommentService
{
    public async Task<List<Comment>> GetAllComments()
    {
        return await commentRepository.GetComments();
    }

    public async Task<Comment?> GetCommentById(int commentId)
    {
        return await commentRepository.GetCommentsById(commentId);
    }

    public async Task<List<Comment>> GetPostComments(int postId)
    {
        return await commentRepository.GetCommentsByPostIdAndParentCommentIsNull(postId);
    }

    public async Task<int> GetPostCommentsCount(int postId)
    {
        var comments = await commentRepository.GetCommentsByPostId(postId);
        return comments.Count;
    }

    public async Task<List<Comment>> GetCommentReplies(int parentCommentId)
    {
        return await commentRepository.GetCommentsByParentCommentId(parentCommentId);
    }

    public async Task CreateComment(AddCommentRequestDto request)
    {
        var user = await userRepository.GetUserByUsername(request.Username);
        var post = await postRepository.GetPostById(request.PostId);

        if (user is null || post is null)
        {
            throw new NotFoundException();
        }

        var comment = new Comment
        {
            Content = request.Content,
            Author = user.UserName,
            User = user,
            Post = post
        };

        await commentRepository.InsertComment(comment);
    }

    public async Task AddReply(int commentId, AddCommentRequestDto request)
    {
        var user = await userRepository.GetUserByUsername(request.Username);
        
        var comment = await commentRepository.GetCommentsById(commentId);
        
        if (user is null || comment is null)
        {
            throw new NotFoundException();
        }
        
        var post = await postRepository.GetPostById(comment.PostId);
        
        if (post is null)
        {
            throw new NotFoundException();
        }

        var reply = new Comment
        {
            Content = request.Content,
            Author = user.UserName,
            User = user,
            Post = post,
            ParentComment = comment
        };

        await commentRepository.InsertComment(reply);
    }

    public async Task UpdateComment(int commentId, UpdateCommentRequestDto request)
    {
        var comment = await commentRepository.GetCommentsById(commentId);
        
        if (comment is null)
        {
            throw new NotFoundException();
        }

        comment.Content = request.Content;

        await commentRepository.UpdateComment(comment);
    }

    public async Task DeleteComment(int commentId)
    {
        await commentRepository.DeleteComment(commentId);
    }
}