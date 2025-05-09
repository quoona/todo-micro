namespace Rabbit.Contracts.LogMessages;

public class CreateTodoMessage
{
    public Guid GuidId { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}