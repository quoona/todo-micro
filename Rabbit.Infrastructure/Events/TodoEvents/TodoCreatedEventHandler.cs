using System.Text.Json;
using MassTransit;
using MediatR;
using Rabbit.Application.Events.TodoEvents;
using Rabbit.Contracts.LogMessages;

namespace Rabbit.Infrastructure.Events.TodoEvents;

public class TodoCreatedEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<TodoCreatedEvent>
{
    public async Task Handle(TodoCreatedEvent notification, CancellationToken cancellationToken)
    {
        var message = new AuditLogMessage
        {
            Action = "CreateAsync",
            UserId = notification.Todo.GuidId.ToString(),
            Data = JsonSerializer.Serialize(notification.Todo),
            Timestamp = DateTime.Now
        };

        await publishEndpoint.Publish(message, cancellationToken);
    }
}