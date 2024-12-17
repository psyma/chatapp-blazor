namespace chatapp_blazor.Models;

public class Message
{
    public int Id { get; set; }
    public required string SenderId { get; set; }
    public required string ReceiverId { get; set; }
    public required string Content { get; set; }
    public required DateTime Timestamp { get; set; }
}