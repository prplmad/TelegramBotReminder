using Telegram.Bot;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System;
using System.Threading;

namespace Business.Abstract.Services
{
    public interface IBotControllerServiceAdditionalMethods
    {
        Task<bool> ChooseStateAsync(ITelegramBotClient botClient, Message message, Models.User user);
        Task AddNoteAsync(ITelegramBotClient botClient, Message message, Models.User user);
        Task DeleteNoteAsync(ITelegramBotClient botClient, Message message, Models.User user);
        Task AddRemindAsync(ITelegramBotClient botClient, Message message, Models.User user);
        Task DeleteRemindAsync(ITelegramBotClient botClient, Message message, Models.User user);
        Task<bool> SetDateAsync(ITelegramBotClient botClient, Message message, Models.User user);
    }
}
