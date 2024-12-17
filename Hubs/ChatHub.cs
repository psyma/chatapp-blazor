using chatapp_blazor.Models;
using Microsoft.AspNetCore.SignalR;

namespace chatapp_blazor.Hubs;

public class ChatHub : Hub
{
    private readonly Utils _utils;
    public ChatHub(Utils utils)
    {
        _utils = utils;
    }
    
    public async Task SendMessage(string senderId, string receiverId, string content)
    {
        if (_utils.ConnectionIds.TryGetValue(receiverId, out var cid))
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
            _utils.Ids.Add(id);
            _utils.ConnectionIds[id] = cid;
            await Clients.All.SendAsync("ConnectedUsers", _utils.Ids);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string? id = Context.GetHttpContext()?.Request.Query["userId"];
        if (id != null)
        {
            _utils.Ids.Remove(id);
            _utils.ConnectionIds.Remove(id);
            await Clients.All.SendAsync("DisconnectedUser", id);
        }
    }
}