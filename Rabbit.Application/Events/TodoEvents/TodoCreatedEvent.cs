using MediatR;
using Rabbit.Application.DTOs;

namespace Rabbit.Application.Events.TodoEvents;

public class TodoCreatedEvent(TodoDto todo) : INotification
{
    public TodoDto Todo { get; } = todo;
}