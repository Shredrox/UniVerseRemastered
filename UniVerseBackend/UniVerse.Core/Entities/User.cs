using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using UniVerse.Core.Enums;

namespace UniVerse.Core.Entities;

public class User : IdentityUser
{
    [MaxLength(256)]
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenValidity { get; set; }
    public UserRole Role { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public bool IsOnline { get; set; }
    public bool IsEnabled { get; set; }
    [JsonIgnore]
    public ICollection<GroupEvent> GroupEvents { get; set; }
    [JsonIgnore]
    public ICollection<JobOffer> JobOffers { get; set; }

    public User()
    {
        GroupEvents = new HashSet<GroupEvent>();
        JobOffers = new HashSet<JobOffer>();
    }
}