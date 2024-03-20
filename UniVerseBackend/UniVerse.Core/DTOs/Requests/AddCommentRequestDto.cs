namespace UniVerse.Core.DTOs.Requests;

public record AddCommentRequestDto(
    int PostId,
    string Content,
    string Username);