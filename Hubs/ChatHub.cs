using chatapp_blazor.Models;
using Microsoft.AspNetCore.SignalR;

namespace chatapp_blazor.Hubs;

public class ChatHub : Hub
{
    private readonly DataDbContext _dbContext;
    private readonly Utils _utils;
    public ChatHub(DataDbContext dbContext, Utils utils)
    {
        _dbContext = dbContext;
        _utils = utils;
    }
    
    public async Task Send(string sender, string receiver, string message)
    {
         
        await Task.Delay(1);
    }
    
    public override async Task OnConnectedAsync()
    {
        string? id = Context.GetHttpContext()?.Request.Query["userId"];
        if (id != null)
        {
            _utils.Ids.Add(id);
            await Clients.All.SendAsync("ConnectedUsers", _utils.Ids);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string? id = Context.GetHttpContext()?.Request.Query["userId"];
        if (id != null)
        {
            _utils.Ids.Remove(id);
            await Clients.All.SendAsync("DisconnectedUser", id);
        }
    }
}