using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class MessageRepository(UniVerseDbContext context) : IMessageRepository
{
    public async Task<List<Message>> GetMessagesBySenderOrReceiver(User user1, User user2)
    {
        return await context.Messages
            .Where(m => m.Sender == user1 && m.Receiver == user2 || (m.Sender == user2 && m.Receiver == user1))
            .ToListAsync();
    }

    public async Task InsertMessage(Message message)
    {
        context.Messages.Add(message);
        await context.SaveChangesAsync();
    }
}