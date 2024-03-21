using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class ChatService(
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IUserRepository userRepository) : IChatService
{
    public async Task<List<MessageResponseDto>> GetChatMessages(string user1Name, string user2Name)
    {
        var user1 = await userRepository.GetUserByUsername(user1Name);
        var user2 = await userRepository.GetUserByUsername(user2Name);

        if (user1 is null || user2 is null)
        {
            throw new NotFoundException();
        }

        var messages = await messageRepository.GetMessagesBySenderOrReceiver(user1, user2);

        var response = messages
            .Select(m => new MessageResponseDto(m.Content, m.Sender.UserName, m.Receiver.UserName, m.Timestamp))
            .ToList();

        return response;
    }

    public async Task<List<ChatResponseDto>> GetUserChats(string username)
    {
        var user = await userRepository.GetUserByUsername(username);
        if (user is null)
        {
            throw new NotFoundException();
        }

        var chats = await chatRepository.GetChatsByUser1IdOrUser2Id(user, user);

        var response = chats
            .Select(c => new ChatResponseDto(c.User1.UserName, c.User2.UserName))
            .ToList();
        
        return response;
    }

    public async Task<ChatResponseDto> CreateChat(CreateChatRequestDto request)
    {
        var user1 = await userRepository.GetUserByUsername(request.User1Name);
        var user2 = await userRepository.GetUserByUsername(request.User2Name);

        if (user1 is null || user2 is null)
        {
            throw new NotFoundException();
        }
        
        var chat = new Chat
        {
            User1 = user1,
            User2 = user2,
            CreatedAt = DateTime.Now.ToUniversalTime()
        };

        await chatRepository.InsertChat(chat);

        return new ChatResponseDto(
            chat.User1.UserName,
            chat.User2.UserName);
    }

    public async Task<MessageResponseDto> CreateMessage(SendMessageRequestDto request)
    {
        var user1 = await userRepository.GetUserByUsername(request.Sender);
        var user2 = await userRepository.GetUserByUsername(request.Receiver);
        
        if (user1 is null || user2 is null)
        {
            throw new NotFoundException();
        }

        var chat = await chatRepository.GetChatByUser1AndUser2(user1, user2);

        if (chat is null)
        {
            throw new NotFoundException();
        }
        
        var message = new Message
        {
            Content = request.Content,
            Chat = chat,
            Sender = user1,
            Receiver = user2,
            Timestamp = DateTime.Now.ToUniversalTime()
        };

        await messageRepository.InsertMessage(message);

        return new MessageResponseDto(
            message.Content, 
            message.Sender.UserName, 
            message.Receiver.UserName,
            message.Timestamp);
    }
}