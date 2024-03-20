using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface ICommentRepository
{
    Task<List<Comment>> GetComments();
    Task<Comment?> GetCommentsById(int id);
    Task<List<Comment>> GetCommentsByPostId(int postId);
    Task<List<Comment>> GetCommentsByPostIdAndParentCommentIsNull(int postId);
    Task<List<Comment>> GetCommentsByParentCommentId(int parentCommentId);
    Task InsertComment(Comment comment);
    Task UpdateComment(Comment comment);
    Task DeleteComment(int commentId);
    Task DeleteCommentsByPostId(int postId);
}