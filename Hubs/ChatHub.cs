using chatapp_blazor.Models;
using Microsoft.AspNetCore.SignalR;

namespace chatapp_blazor.Hubs;

public class ChatHub(Utility utility) : Hub
{
    public async Task SendMessage(string senderId, string receiverId, string content)
    {
        if (utility.ConnectionIdMap.TryGetValue(receiverId, out var cid))
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
            utility.Ids.Add(id);
            utility.ConnectionIdMap[id] = cid; 
        }

        await Task.Delay(1);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string? id = Context.GetHttpContext()?.Request.Query["userId"];
        if (id != null)
        {
            utility.Ids.Remove(id);
            utility.ConnectionIdMap.Remove(id);
            await Clients.All.SendAsync("DisconnectedUser", id);
        }
    }
}