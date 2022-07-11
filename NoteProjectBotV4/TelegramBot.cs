using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;
using Business.Abstract.Services;


namespace NoteBot
{
    public class TelegramBot
    {
        private readonly IConfiguration _configuration;
        private TelegramBotClient _botClient;
        private readonly ITelegramBotService _telegramBotService;

        private TelegramBot()
        {

        }
        public TelegramBot(IConfiguration configuration, ITelegramBotService telegramBotService)
        {
            _configuration = configuration;
            _telegramBotService = telegramBotService;
        }

        public async Task<TelegramBotClient> GetBot()
        {
            if (_botClient != null)
            {
                return _botClient;
            }
            _botClient = new TelegramBotClient(_configuration["Token"]);  //Token from appsettings.json
            var hook = $"{_configuration["Url"]}api/message"; //URL from appsettings.json
            await _botClient.SetWebhookAsync(hook);
            await RemindSender();
            return _botClient;
        }

        public async Task RemindSender()
        {
            await _telegramBotService.RemindSender(_botClient);
        }
    }
}
