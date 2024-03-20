using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IServices;

public interface ICommentService
{
    Task<List<Comment>> GetAllComments();
    Task<Comment?> GetCommentById(int commentId);
    Task<List<Comment>> GetPostComments(int postId);
    Task<int> GetPostCommentsCount(int postId);
    Task<List<Comment>> GetCommentReplies(int parentCommentId);
    Task CreateComment(AddCommentRequestDto request);
    Task AddReply(int commentId, AddCommentRequestDto request);
    Task UpdateComment(int commentId, UpdateCommentRequestDto request);
    Task DeleteComment(int commentId);
}