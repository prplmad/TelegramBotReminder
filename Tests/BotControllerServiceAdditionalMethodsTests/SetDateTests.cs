using Telegram.Bot;
using Telegram.Bot.Types;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Business.Abstract.Services;
using Business.Abstract.Wrappers;
using Business.Services;
using System;

namespace Tests.BotControllerServiceAdditionalMethodsTests
{
    [TestFixture]
    public class SetDateTests
    {
        private Mock<IChooseStateAdditionalMethods> _chooseStateAdditionalMethods;
        private Mock<IUsersService> _usersService;
        private Mock<INotesService> _notesService;
        private Mock<IRemindsService> _remindsService;
        private Mock<IStatesService> _statesService;
        private Mock<ITelegramBotClientWrapper> _telegramBotClientWrapper;
        private BotControllerServiceAdditionalMethods _botControllerServiceAdditionalMethods;
        private Mock<TelegramBotClient> _botClient;
        private Business.Models.User _user;
        private Message _message;

        [SetUp]
        public void Init()
        {
            _botClient = new Mock<TelegramBotClient>("SomeToken", null, default);
            _usersService = new Mock<IUsersService>();
            _notesService = new Mock<INotesService>();
            _statesService = new Mock<IStatesService>();
            _remindsService = new Mock<IRemindsService>();
            _telegramBotClientWrapper = new Mock<ITelegramBotClientWrapper>();
            _chooseStateAdditionalMethods = new Mock<IChooseStateAdditionalMethods>();
            _botControllerServiceAdditionalMethods = new BotControllerServiceAdditionalMethods(_notesService.Object, _remindsService.Object, _statesService.Object, _telegramBotClientWrapper.Object, _chooseStateAdditionalMethods.Object);
            _user = new Business.Models.User();
            _message = new Message();
        }

        [Test]
        public async Task SetDate_IncorrectDateFormat()
        {
            //Arrange
            _message.Text = "2029-03-01 20:00"; // Некорректный формат даты

            //Act
            await _botControllerServiceAdditionalMethods.SetDate(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Некорректный формат даты, введите заново!\nФормат даты: dd.mm.yyyy hh:mm"));
        }
        [Test]
        public async Task SetDate_CorrectDateFormat()
        {
            //Arrange
            _message.Text = "07.01.2029 20:00"; // Корректный формат даты

            //Act
            await _botControllerServiceAdditionalMethods.SetDate(_botClient.Object, _message, _user);

            //Verify
            _remindsService.Verify(sd => sd.SetDate(It.IsAny<Business.Models.User>(), It.IsAny<DateTime>()));
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Напоминание успешно создано"));
        }

        [Test]
        public async Task SetDate_DateInThePast()
        {
            //Arrange
            _message.Text = "07.01.2021 20:00"; // Корректный формат даты

            //Act
            await _botControllerServiceAdditionalMethods.SetDate(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Дата не может быть в прошлом, введите заново!\nФормат даты: dd.mm.yyyy hh:mm"));
        }
    }
}
