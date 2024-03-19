﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UniVerse.Core.Entities;

public class User : IdentityUser
{
    [MaxLength(256)]
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenValidity { get; set; }
}