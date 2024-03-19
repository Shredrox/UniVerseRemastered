namespace UniVerse.Core.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public byte[] ImageData { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}