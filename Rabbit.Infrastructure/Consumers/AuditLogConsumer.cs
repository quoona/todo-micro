using MassTransit;
using Rabbit.Application.Interfaces;
using Rabbit.Application.Messages;

namespace Rabbit.Infrastructure.Consumers;

public class AuditLogConsumer(ILoggingService loggingService) : IConsumer<AuditLogMessage>
{
    public async Task Consume(ConsumeContext<AuditLogMessage> context)
    {
        var message = context.Message;
        loggingService.LogInformation(
            $"[AUDIT] Action: {message.Action}, User: {message.UserId}, Time: {message.Timestamp}");

        //TODO: Save to DB if u want
        await Task.CompletedTask;
    }
}