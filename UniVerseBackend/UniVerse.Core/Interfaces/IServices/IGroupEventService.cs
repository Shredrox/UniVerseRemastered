using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IServices;

public interface IGroupEventService
{
    Task<List<GroupEvent>> GetAllGroupEvents();
    Task<List<GroupEvent>> GetTrendingGroupEvents();
    Task<GroupEvent?> GetGroupEventById(int groupEventId);
    Task<bool> IsAttending(int groupEventId, string username);
    Task AttendGroupEvent(int groupEventId, string username);
    Task RemoveAttending(int groupEventId, string username);
    Task CreateGroupEvent(CreateGroupEventRequestDto request);
    Task UpdateGroupEvent(int groupEventId, UpdateGroupEventRequestDto request);
    Task DeleteGroupEvent(int groupEventId);
}