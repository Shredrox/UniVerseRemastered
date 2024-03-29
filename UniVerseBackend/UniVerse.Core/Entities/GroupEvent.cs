﻿using System.Text.Json.Serialization;

namespace UniVerse.Core.Entities;

public class GroupEvent
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int OrganiserId { get; set; }
    public Organiser Organiser { get; set; }
    public DateTime Date { get; set; }
    [JsonIgnore]
    public ICollection<User> Attendees { get; set; }

    public GroupEvent()
    {
        Attendees = new HashSet<User>();
    }
}