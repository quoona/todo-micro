namespace Rabbit.Domain.Entities;

public class Todo
{
    public int TodoId { get; set; }
    public Guid GuildId { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}