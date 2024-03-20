using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IGroupEventRepository
{
    Task<List<GroupEvent>> GetGroupEvents();
    Task<GroupEvent?> GetGroupEventById(int id);
    Task InsertGroupEvent(GroupEvent groupEvent);
    Task UpdateGroupEvent(GroupEvent groupEvent);
    Task DeleteGroupEvent(int groupEventId);
}