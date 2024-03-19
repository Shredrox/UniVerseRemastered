using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;

namespace UniVerse.Infrastructure.Data;

public class UniVerseDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    public DbSet<GroupEvent> GroupEvents { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
}