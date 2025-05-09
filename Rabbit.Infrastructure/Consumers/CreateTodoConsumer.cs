using MassTransit;
using Rabbit.Application.Interfaces.Todos;
using Rabbit.Contracts.LogMessages;
using Rabbit.Domain.Entities;

namespace Rabbit.Infrastructure.Consumers;

public class CreateTodoConsumer(ITodoRepository repository) : IConsumer<CreateTodoMessage>
{
    public async Task Consume(ConsumeContext<CreateTodoMessage> context)
    {
        var msg = context.Message;

        var todo = new Todo(msg.Title)
        {
            GuidId = msg.GuidId,
            Title = msg.Title,
            IsCompleted = msg.IsCompleted
        };

        await repository.AddAsync(todo);
    }
}