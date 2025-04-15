
using Telegram.Bot;

namespace Rabbit.Audit.Application.Telegram
{
    public class TelegramBotNotification
    {
        public async Task SendMessTelegramAsync(string message)
        {
            // create bot instance
            var bot = new TelegramBotClient("8036970809:AAGRdwhiwG1I20Y2w6PJwNij41Sumg2aSHg");

            if (message.Length > 4096) message = message.Substring(0, 4096);

            await bot.SendMessage("-1002457511131", message);
        }
    }
}