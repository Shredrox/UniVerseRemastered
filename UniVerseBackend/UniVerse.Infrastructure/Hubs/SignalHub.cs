using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using UniVerse.Core.Interfaces.IClients;

namespace UniVerse.Infrastructure.Hubs;

[Authorize]
public class SignalHub : Hub<ISignalClient>
{
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
    }
}