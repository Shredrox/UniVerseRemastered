namespace UniVerse.Core.Entities;

public class JobOffer
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Requirements { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public string Salary { get; set; }
    public string EmployerId { get; set; }
    public User Employer { get; set; }
    public ICollection<User> Applicants { get; set; }
}