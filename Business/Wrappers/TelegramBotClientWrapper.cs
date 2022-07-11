using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Business.Abstract.Wrappers;

namespace Business
{
    public class TelegramBotClientWrapper : ITelegramBotClientWrapper
    {
        public async Task<Message> SendTextMessageAsync(ITelegramBotClient botClient, ChatId chatId, string text)
        {
            return await botClient.SendTextMessageAsync(chatId, text);
        }
    }
}
