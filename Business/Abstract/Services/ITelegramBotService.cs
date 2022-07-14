using Telegram.Bot;
using System.Threading.Tasks;



namespace Business.Abstract.Services
{
    public interface ITelegramBotService
    {
        Task RemindSenderAsync(ITelegramBotClient botClient);
    }
}
