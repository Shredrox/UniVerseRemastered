namespace UniVerse.Core.Entities;

public class News
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public byte[] ImageData { get; set; }
    public bool Pinned { get; set; }
    public DateTime Date { get; set; }
}