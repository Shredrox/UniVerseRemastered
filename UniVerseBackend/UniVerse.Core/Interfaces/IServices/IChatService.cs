using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;

namespace UniVerse.Core.Interfaces.IServices;

public interface IChatService
{
    Task<List<MessageResponseDto>> GetChatMessages(string user1Name, string user2Name);
    Task<List<ChatResponseDto>> GetUserChats(string username);
    Task<ChatResponseDto> CreateChat(CreateChatRequestDto request);
    Task<MessageResponseDto> CreateMessage(SendMessageRequestDto request);
}