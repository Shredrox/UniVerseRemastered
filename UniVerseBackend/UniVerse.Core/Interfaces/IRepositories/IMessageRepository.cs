using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IMessageRepository
{
    Task<List<Message>> GetMessagesBySenderOrReceiver(User user1, User user2);
    Task InsertMessage(Message message);
}