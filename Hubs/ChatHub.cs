using Microsoft.AspNetCore.SignalR;

namespace chatapp_blazor.Hubs;

public class ChatHub : Hub
{
    public async Task Send(string sender, string receiver, string message)
    {
        string? id = Context.GetHttpContext()?.Request.Query["userId"];
        Console.WriteLine($"{sender}: {receiver}: {message}: {id}");
        await Task.Delay(1);
    }
    
    public override async Task OnConnectedAsync()
    {
         
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
         
    }
}