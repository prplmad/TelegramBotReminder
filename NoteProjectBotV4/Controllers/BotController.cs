using Microsoft.AspNetCore.Mvc;
using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Business.Abstract.Services;
using NoteBot.Mappers;
using NoteBot;

namespace NoteBot.Controllers
{
    [Route("api/message")]
    public class BotController : ControllerBase
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly IBotControllerService _botControllerService;

        public BotController(TelegramBot telegramBot, IBotControllerService botControllerService)
        {
            _telegramBotClient = telegramBot.GetBotAsync().Result;
            _botControllerService = botControllerService;
        }

        [HttpPost]
        public async Task<IActionResult> HandleUpdateAsync([FromBody] Update update, CancellationToken cancellationToken)
        {
            var user = update.Message.From.FromApiToBusiness();
            var handler = update.Type switch
            {
                UpdateType.Message => _botControllerService.BotOnMessageReceivedAsync(_telegramBotClient, update.Message, user),
                UpdateType.EditedMessage => _botControllerService.BotOnMessageReceivedAsync(_telegramBotClient, update.EditedMessage, user),
                _ => throw new NotImplementedException()
            };
            try
            {
                await handler;
                return Ok();
            }
            catch (Exception exception)
            {
                await _botControllerService.HandleErrorAsync(_telegramBotClient, exception, cancellationToken);
                return Ok();
            }
        }
    }
}
