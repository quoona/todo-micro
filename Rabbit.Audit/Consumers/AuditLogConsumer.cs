using MassTransit;
using Rabbit.Audit.Application.Interfaces;
using Rabbit.Contracts.Contracts;

namespace Rabbit.Audit.Consumers;

public class AuditLogConsumer(ILoggingService loggingService, ITelegramMessageService messageService)
    : IConsumer<AuditLogMessage>
{
    public async Task Consume(ConsumeContext<AuditLogMessage> context)
    {
        var message = context.Message;
        loggingService.LogInformation(
            $"[AUDIT] Action: {message.Action}, User: {message.UserId}, Time: {message.Timestamp}");

        await messageService.SendMessage(
            $"[0_AUDIT] Action: {message.Action}, User: {message.UserId}, Time: {message.Timestamp}");

        await Task.CompletedTask;
    }
}