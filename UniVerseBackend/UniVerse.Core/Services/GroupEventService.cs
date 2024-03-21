using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class GroupEventService(
    IGroupEventRepository groupEventRepository,
    IUserRepository userRepository,
    IOrganiserRepository organiserRepository) : IGroupEventService
{
    public async Task<List<GroupEventResponseDto>> GetAllGroupEvents()
    {
        var groupEvents = await groupEventRepository.GetGroupEvents();

        return groupEvents
            .Select(e => new GroupEventResponseDto(
                e.Id, 
                e.Title, 
                e.Description, 
                e.Date.ToString("dd-MM-yyyy HH:mm"))
            )
            .ToList();
    }

    public async Task<List<GroupEventResponseDto>> GetTrendingGroupEvents()
    {
        var groupEvents = await groupEventRepository.GetGroupEvents();

        var trendingGroupEvents = groupEvents
            .Where(e => e.Attendees.Count > 0)
            .ToList();

        return trendingGroupEvents
            .Select(e => new GroupEventResponseDto(
                e.Id, 
                e.Title, 
                e.Description, 
                e.Date.ToString("dd-MM-yyyy HH:mm"))
            )
            .ToList();
    }

    public async Task<GroupEventResponseDto?> GetGroupEventById(int groupEventId)
    {
        var groupEvent = await groupEventRepository.GetGroupEventById(groupEventId);

        if (groupEvent is null)
        {
            throw new NotFoundException();
        }
        
        return new GroupEventResponseDto(
            groupEvent.Id, 
            groupEvent.Title, 
            groupEvent.Description,
            groupEvent.Date.ToString("dd-MM-yyyy HH:mm")
            );
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
        var organiser = await organiserRepository.GetOrganiserByName(request.OrganiserName);

        if (organiser is null)
        {
            throw new NotFoundException();
        }

        var groupEvent = new GroupEvent
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Organiser = organiser
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