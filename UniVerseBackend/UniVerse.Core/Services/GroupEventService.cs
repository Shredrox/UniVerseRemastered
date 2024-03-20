using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class GroupEventService(
    IGroupEventRepository groupEventRepository,
    IUserRepository userRepository) : IGroupEventService
{
    public async Task<List<GroupEvent>> GetAllGroupEvents()
    {
        return await groupEventRepository.GetGroupEvents();
    }

    public async Task<List<GroupEvent>> GetTrendingGroupEvents()
    {
        var groupEvents = await groupEventRepository.GetGroupEvents();

        var trendingGroupEvents = groupEvents
            .Where(e => e.Attendees.Count > 10)
            .ToList();

        return trendingGroupEvents;
    }

    public async Task<GroupEvent?> GetGroupEventById(int groupEventId)
    {
        return await groupEventRepository.GetGroupEventById(groupEventId);
    }

    public async Task<bool> IsAttending(int groupEventId, string username)
    {
        var groupEvent = await groupEventRepository.GetGroupEventById(groupEventId);
        var user = await userRepository.GetUserByUsername(username);
        
        if (groupEvent is null || user is null)
        {
            throw new NotFoundException();
        }
        
        return groupEvent.Attendees.Any(a => a.Equals(user));
    }

    public async Task AttendGroupEvent(int groupEventId, string username)
    {
        var groupEvent = await groupEventRepository.GetGroupEventById(groupEventId);
        var user = await userRepository.GetUserByUsername(username);
        
        if (groupEvent is null || user is null)
        {
            throw new NotFoundException();
        }
        
        groupEvent.Attendees.Add(user);
        await groupEventRepository.UpdateGroupEvent(groupEvent);
    }

    public async Task RemoveAttending(int groupEventId, string username)
    {
        var groupEvent = await groupEventRepository.GetGroupEventById(groupEventId);
        var user = await userRepository.GetUserByUsername(username);
        
        if (groupEvent is null || user is null)
        {
            throw new NotFoundException();
        }
        
        groupEvent.Attendees.Remove(user);
        await groupEventRepository.UpdateGroupEvent(groupEvent);
    }

    public async Task CreateGroupEvent(CreateGroupEventRequestDto request)
    {
        var user = await userRepository.GetUserByUsername(request.OrganiserName);

        if (user is null)
        {
            throw new NotFoundException();
        }

        var groupEvent = new GroupEvent
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Organiser = user
        };

        await groupEventRepository.InsertGroupEvent(groupEvent);
    }

    public async Task UpdateGroupEvent(int groupEventId, UpdateGroupEventRequestDto request)
    {
        var groupEvent = await groupEventRepository.GetGroupEventById(groupEventId);
        
        if (groupEvent is null)
        {
            throw new NotFoundException();
        }
        
        if (!string.IsNullOrEmpty(request.Title))
        {
            groupEvent.Title = request.Title;
        }
        if (!string.IsNullOrEmpty(request.Description))
        {
            groupEvent.Description = request.Description;
        }

        groupEvent.Date = request.Date;

        await groupEventRepository.UpdateGroupEvent(groupEvent);
    }

    public async Task DeleteGroupEvent(int groupEventId)
    {
        await groupEventRepository.DeleteGroupEvent(groupEventId);
    }
}