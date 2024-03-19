namespace UniVerse.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public int? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}