
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;


namespace Business.Abstract.Wrappers
{
    public interface IMessageWrapper
    {
        Task<bool> FromBot(ITelegramBotClient botClient, Message message);
    }
}
