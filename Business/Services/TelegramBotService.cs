using Telegram.Bot;
using System.Threading.Tasks;
using Business.Abstract.Services;
using System.Threading;

namespace Business.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly IRemindsService _remindsService;
        public TelegramBotService(IRemindsService remindsService)
        {
            _remindsService = remindsService;
        }

        public async Task RemindSenderAsync(ITelegramBotClient botClient)
        {
            while (true)
            {
                await _remindsService.SendRemindAsync(botClient);
                Thread.Sleep(5000);
            }
        }
    }
}
