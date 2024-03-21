using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IServices;

public interface INotificationService
{
    Task<List<NotificationResponseDto>> GetUserNotifications(string username);
    Task ReadNotifications(string username);
    Task<NotificationResponseDto> CreateNotification(AddNotificationRequestDto request);
}