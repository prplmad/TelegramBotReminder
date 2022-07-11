using Telegram.Bot;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System;
using System.Threading;

namespace Business.Abstract.Services
{
    public interface IBotControllerServiceAdditionalMethods
    {
        Task<bool> ChooseState(ITelegramBotClient botClient, Message message, Models.User user);
        Task AddNote(ITelegramBotClient botClient, Message message, Models.User user);
        Task DeleteNote(ITelegramBotClient botClient, Message message, Models.User user);
        Task AddRemind(ITelegramBotClient botClient, Message message, Models.User user);
        Task DeleteRemind(ITelegramBotClient botClient, Message message, Models.User user);
        Task<bool> SetDate(ITelegramBotClient botClient, Message message, Models.User user);
    }
}
