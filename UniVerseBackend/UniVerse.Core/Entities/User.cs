﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using UniVerse.Core.Enums;

namespace UniVerse.Core.Entities;

public class User : IdentityUser
{
    [MaxLength(256)]
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenValidity { get; set; }
    public UserRole Role { get; set; }
    public byte[] ProfilePicture { get; set; }
    public bool IsOnline { get; set; }
    public bool IsEnabled { get; set; }
    public ICollection<GroupEvent> GroupEvents { get; set; }
    public ICollection<JobOffer> JobOffers { get; set; }
}