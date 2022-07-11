using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;

namespace Business.Abstract.Wrappers
{
    public interface ITelegramBotClientWrapper
    {
        Task<Message> SendTextMessageAsync(
            ITelegramBotClient botClient,
            ChatId chatId,
            string text
        );
    }
}
