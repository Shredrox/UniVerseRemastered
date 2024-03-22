using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IClients;

public interface ISignalClient
{   
    Task ReceiveMessage(MessageResponseDto message);
    Task ChatCreated(ChatResponseDto chat);
    Task ReceiveNotification(NotificationResponseDto notification);
    Task ReceiveFriendRequest(FriendRequestResponseDto friendRequest);
    Task ReceiveOnlineAlert(string username);
}