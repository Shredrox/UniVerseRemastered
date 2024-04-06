using System.Text.Json.Serialization;

namespace UniVerse.Core.Entities;

public class JobOffer
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }
    public string Requirements { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public string Salary { get; set; }
    public int EmployerId { get; set; }
    public Employer Employer { get; set; }
    [JsonIgnore]
    public ICollection<User> Applicants { get; set; }

    public JobOffer()
    {
        Applicants = new HashSet<User>();
    }
}