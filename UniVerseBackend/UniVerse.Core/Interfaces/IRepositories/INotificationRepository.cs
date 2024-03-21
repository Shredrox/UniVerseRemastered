using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface INotificationRepository
{
    Task<List<Notification>> GetNotificationsByUser(User user);
    Task InsertNotification(Notification notification);
    Task UpdateNotifications(List<Notification> notifications);
}