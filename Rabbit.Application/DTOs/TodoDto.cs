namespace Rabbit.Application.DTOs;

public class TodoDto
{
    public int GuidId { get; set; }
    public string Title { get; set; }
    public bool IsComplete { get; set; }
}