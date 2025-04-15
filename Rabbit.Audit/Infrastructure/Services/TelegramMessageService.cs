using Rabbit.Audit.Application.Interfaces;
using Rabbit.Audit.Application.Telegram;

namespace Rabbit.Audit.Infrastructure.Services;

public class TelegramMessageService : ITelegramMessageService
{
    public async Task SendMessage(string message)
    {
        var telegramBot = new TelegramBotNotification();
        await telegramBot.SendMessTelegramAsync(message);
    }
}