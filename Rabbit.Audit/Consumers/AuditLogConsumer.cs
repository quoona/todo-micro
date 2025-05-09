using MassTransit;
using Rabbit.Audit.Application.Interfaces;
using Rabbit.Contracts.LogMessages;

namespace Rabbit.Audit.Consumers;

public class AuditLogConsumer(
    ILoggingService loggingService,
    ITelegramMessageService messageService,
    ILogger<AuditLogConsumer> logger)
    : IConsumer<AuditLogMessage>
{
    public async Task Consume(ConsumeContext<AuditLogMessage> context)
    {
        var message = context.Message;
        loggingService.LogInformation(
            $"[AUDIT] Action: {message.Action}, User: {message.UserId}, Time: {message.Timestamp}, Data: {message.Data}");

        logger.LogDebug(
            $"[AUDIT] Action: {message.Action}, User: {message.UserId}, Time: {message.Timestamp}, Data: {message.Data}");
        // await messageService.SendMessage(
        //     $"[0_AUDIT] Action: {message.Action}, User: {message.UserId}, Time: {message.Timestamp}, Data: {message.Data}");

        await Task.CompletedTask;
    }
}