using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class GroupEventRepository(UniVerseDbContext context) : IGroupEventRepository
{
    public async Task<List<GroupEvent>> GetGroupEvents()
    {
        return await context.GroupEvents.ToListAsync();
    }

    public async Task<GroupEvent?> GetGroupEventById(int id)
    {
        return await context.GroupEvents.FindAsync(id);
    }

    public async Task InsertGroupEvent(GroupEvent groupEvent)
    {
        context.GroupEvents.Add(groupEvent);
        await context.SaveChangesAsync();
    }

    public async Task UpdateGroupEvent(GroupEvent groupEvent)
    {
        context.Entry(groupEvent).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteGroupEvent(int groupEventId)
    {
        var groupEvent = await context.GroupEvents.FindAsync(groupEventId);
        
        if (groupEvent is null)
        {
            throw new NotFoundException();
        }
        
        context.GroupEvents.Remove(groupEvent);
        await context.SaveChangesAsync();
    }
}