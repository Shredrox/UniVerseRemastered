namespace UniVerse.Core.Entities;

public class Employer
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}