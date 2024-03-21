using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class NotificationRepository(UniVerseDbContext context) : INotificationRepository
{
    public async Task<List<Notification>> GetNotificationsByUser(User user)
    {
        return await context.Notifications
            .Where(n => n.User == user)
            .ToListAsync();
    }

    public async Task InsertNotification(Notification notification)
    {
        context.Notifications.Add(notification);
        await context.SaveChangesAsync();
    }

    public async Task UpdateNotifications(List<Notification> notifications)
    {
        context.Notifications.UpdateRange(notifications);
        await context.SaveChangesAsync();
    }
}