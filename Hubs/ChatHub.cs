using chatapp_blazor.Models;
using Microsoft.AspNetCore.SignalR;

namespace chatapp_blazor.Hubs;

public class ChatHub : Hub
{
    private readonly Utility _utility;
    public ChatHub(Utility utility)
    {
        _utility = utility;
    }
    
    public async Task SendMessage(string senderId, string receiverId, string content)
    {
        if (_utility.ConnectionIdMap.TryGetValue(receiverId, out var cid))
        { 
            await Clients.Client(cid).SendAsync("ReceivedMessage", senderId, receiverId, content);
        }
    }
    
    public override async Task OnConnectedAsync()
    {
        var cid = Context.ConnectionId;
        string? id = Context.GetHttpContext()?.Request.Query["userId"];
        if (id != null)
        {
            _utility.Ids.Add(id);
            _utility.ConnectionIdMap[id] = cid; 
        }

        await Task.Delay(1);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string? id = Context.GetHttpContext()?.Request.Query["userId"];
        if (id != null)
        {
            _utility.Ids.Remove(id);
            _utility.ConnectionIdMap.Remove(id);
            await Clients.All.SendAsync("DisconnectedUser", id);
        }
    }
}