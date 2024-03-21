using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class ChatRepository(UniVerseDbContext context) : IChatRepository
{
    public async Task<List<Chat>> GetChatsByUser1IdOrUser2Id(User user1, User user2)
    {
        return await context.Chats
            .Include(c => c.User1)
            .Include(c => c.User2)
            .Where(c => c.User1 == user1 || c.User2 == user2)
            .ToListAsync();
    }

    public async Task<Chat?> GetChatByUser1AndUser2(User user1, User user2)
    {
        return await context.Chats
            .FirstOrDefaultAsync(c => (c.User1 == user1 && c.User2 == user2) || c.User1 == user2 && c.User2 == user1);
    }

    public async Task InsertChat(Chat chat)
    {
        context.Chats.Add(chat);
        await context.SaveChangesAsync();
    }
}