using System;
using Telegram.Bot;
using Business.Models;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Business.Abstract.Services;
using Business.Abstract.Wrappers;
using Microsoft.Extensions.Logging;

namespace Business.Services
{
    public class BotControllerService : IBotControllerService
    {
        private readonly IUsersService _usersService;
        private readonly IBotControllerServiceAdditionalMethods _botControllerServiceAdditionalMethods;
        private readonly IMessageWrapper _messageWrapper;
        private readonly ILogger _logger;
        public BotControllerService(IUsersService usersService, IBotControllerServiceAdditionalMethods botControllerServiceAdditionalMethods, IMessageWrapper messageWrapper, ILogger<BotControllerService> logger)
        {
            _usersService = usersService;
            _botControllerServiceAdditionalMethods = botControllerServiceAdditionalMethods;
            _messageWrapper = messageWrapper;
            _logger = logger;
        }

        public async Task BotOnMessageReceivedAsync(ITelegramBotClient botClient, Message message, Models.User user)
        {
            State state = State.None;

            if (await _messageWrapper.FromBot(botClient, message) is false)
            {
                bool doesUserExist = await _usersService.DoesUserExistAsync(user); // вызываем метод проверки пользователя на наличие в БД
                if (doesUserExist)
                {
                    state = await _usersService.GetStateAsync(user);
                }
                else
                {
                    await _usersService.AddUserAsync(user);
                }

            }

            switch (state)
            {
                case State.None:
                    await _botControllerServiceAdditionalMethods.ChooseStateAsync(botClient, message, user);
                    break;
                case State.Note:
                    await _botControllerServiceAdditionalMethods.AddNoteAsync(botClient, message, user);
                    break;
                case State.DeleteNote:
                    await _botControllerServiceAdditionalMethods.DeleteNoteAsync(botClient, message, user);
                    break;
                case State.Remind:
                    await _botControllerServiceAdditionalMethods.AddRemindAsync(botClient, message, user);
                    break;
                case State.DeleteRemind:
                    await _botControllerServiceAdditionalMethods.DeleteRemindAsync(botClient, message, user);
                    break;
                case State.SetDate:
                    await _botControllerServiceAdditionalMethods.SetDateAsync(botClient, message, user);
                    break;
            }
            _logger.LogInformation($"Recieved message from user {user.Id} is \n \"{message.Text}\"");
            if (message.Type != MessageType.Text)
                return;
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            _logger.LogError(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}
