namespace UniVerse.Core.Entities;

public class Notification
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string Type { get; set; }
    public string Source { get; set; }
    public string RecipientName { get; set; }
    public bool IsRead { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime Timestamp { get; set; }
}