using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using UniVerse.Core.Interfaces.IClients;

namespace UniVerse.Infrastructure.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
    }
}