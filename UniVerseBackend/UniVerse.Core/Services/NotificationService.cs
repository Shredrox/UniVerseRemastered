using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class NotificationService(
    INotificationRepository notificationRepository,
    IUserRepository userRepository) : INotificationService
{
    public async Task<List<NotificationResponseDto>> GetUserNotifications(string username)
    {
        var user = await userRepository.GetUserByUsername(username);

        if (user is null)
        {
            throw new NotFoundException();
        }

        var notifications = await notificationRepository.GetNotificationsByUser(user);

        var response = notifications
            .Select(n => new NotificationResponseDto(n.Message, n.IsRead))
            .ToList();
        
        return response;
    }

    public async Task ReadNotifications(string username)
    {
        var user = await userRepository.GetUserByUsername(username);

        if (user is null)
        {
            throw new NotFoundException();
        }

        var notifications = await notificationRepository.GetNotificationsByUser(user);
        
        notifications.ForEach(n => n.IsRead = true);

        await notificationRepository.UpdateNotifications(notifications);
    }
    
    public async Task<NotificationResponseDto> CreateNotification(AddNotificationRequestDto request)
    {
        var user = await userRepository.GetUserByUsername(request.RecipientName);

        if (user is null)
        {
            throw new NotFoundException();
        }

        var notification = new Notification
        {
            Message = request.Message,
            Type = request.Type,
            Source = request.Source,
            RecipientName = request.RecipientName,
            User = user,
            IsRead = false,
            Timestamp = DateTime.Now.ToUniversalTime()
        };

        await notificationRepository.InsertNotification(notification);

        return new NotificationResponseDto(notification.Message, notification.IsRead);
    }

    
}