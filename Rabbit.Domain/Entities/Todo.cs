namespace Rabbit.Domain.Entities;

public class Todo(string title)
{
    public int TodoId { get; set; }
    public Guid GuidId { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = title;
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}