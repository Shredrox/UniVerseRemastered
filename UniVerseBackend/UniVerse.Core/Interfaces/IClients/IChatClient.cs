using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IClients;

public interface IChatClient
{   
    Task ReceiveMessage(MessageResponseDto message);
    Task ChatCreated(ChatResponseDto chat);
    Task ReceiveNotification(NotificationResponseDto notification);
    Task ReceiveFriendRequest(FriendRequestResponseDto friendRequest);
}