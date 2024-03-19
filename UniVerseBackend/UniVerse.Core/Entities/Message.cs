namespace UniVerse.Core.Entities;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
    public string SenderId { get; set; }
    public User Sender { get; set; }
    public string ReceiverId { get; set; }
    public User Receiver { get; set; }
    public DateTime Timestamp { get; set; }
}