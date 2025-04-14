namespace Rabbit.Application.Messages;

public class AuditLogMessage
{
    public string Action { get; set; }
    public string UserId { get; set; }
    public object? Data { get; set; }
    public DateTime Timestamp { get; set; }
}