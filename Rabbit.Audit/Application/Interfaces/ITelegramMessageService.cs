namespace Rabbit.Audit.Application.Interfaces;

public interface ITelegramMessageService
{
    Task SendMessage(string message);
}