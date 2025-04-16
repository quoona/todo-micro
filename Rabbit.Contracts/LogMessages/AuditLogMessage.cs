namespace Rabbit.Contracts.LogMessages;

public class AuditLogMessage
{
    public string Action { get; set; }
    public string UserId { get; set; }
    public object? Data { get; set; }
    public DateTime Timestamp { get; set; }
}