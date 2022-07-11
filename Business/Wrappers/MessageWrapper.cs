using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Business.Abstract.Wrappers;

namespace Business.Services
{
    public class MessageWrapper : IMessageWrapper
    {
        public Task<bool> FromBot(ITelegramBotClient botClient, Message message)
        {
            return Task.FromResult(message.From.IsBot);
        }
    }
}
