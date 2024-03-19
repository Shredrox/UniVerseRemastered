using UniVerse.Core.Enums;

namespace UniVerse.Core.Entities;

public class Friendship
{
    public int Id { get; set; }
    public string User1Id { get; set; }
    public User User1 { get; set; }
    public string User2Id { get; set; }
    public User User2 { get; set; }
    public DateTime Date { get; set; }
    public FriendshipStatus FriendshipStatus { get; set; }
}