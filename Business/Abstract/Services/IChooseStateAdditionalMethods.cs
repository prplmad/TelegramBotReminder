using System;
using Telegram.Bot;
using System.Threading.Tasks;
using Telegram.Bot.Types;


namespace Business.Abstract.Services
{
    public interface IChooseStateAdditionalMethods
    {
        Task GetNotes(ITelegramBotClient telegramBot, Message message, Models.User user);
        Task GetReminds(ITelegramBotClient telegramBot, Message message, Models.User user);
    }
}
