using Telegram.Bot;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System;
using System.Threading;


namespace Business.Abstract.Services
{
    public interface IBotControllerService
    {
        Task BotOnMessageReceived(ITelegramBotClient botClient, Message message, Business.Models.User user);
        Task HandleErrorAsync(ITelegramBotClient telegramBot, Exception exception, CancellationToken cancellationToken);
    }
}
