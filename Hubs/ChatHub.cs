using chatapp_blazor.Data;
using Microsoft.AspNetCore.SignalR;

namespace chatapp_blazor.Hubs;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ApplicationUtils _applicationUtils;
    public ChatHub(ApplicationDbContext dbContext, ApplicationUtils applicationUtils)
    {
        _dbContext = dbContext;
        _applicationUtils = applicationUtils;
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
            _applicationUtils.Ids.Add(id);
            await Clients.All.SendAsync("ConnectedUsers", _applicationUtils.Ids);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string? id = Context.GetHttpContext()?.Request.Query["userId"];
        if (id != null)
        {
            _applicationUtils.Ids.Remove(id);
            await Clients.All.SendAsync("DisconnectedUser", id);
        }
    }
}