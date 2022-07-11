using Telegram.Bot;
using Telegram.Bot.Types;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Business.Abstract.Services;
using Business.Abstract.Wrappers;
using Business.Services;

namespace Tests.BotControllerServiceAdditionalMethodsTests
{
    [TestFixture]
    public class AddRemindTests
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
            _chooseStateAdditionalMethods = new Mock<IChooseStateAdditionalMethods>();
            _telegramBotClientWrapper = new Mock<ITelegramBotClientWrapper>();
            _botControllerServiceAdditionalMethods = new BotControllerServiceAdditionalMethods(_notesService.Object, _remindsService.Object, _statesService.Object, _telegramBotClientWrapper.Object, _chooseStateAdditionalMethods.Object);
            _user = new Business.Models.User();
            _message = new Message();
        }


        [Test]
        public async Task AddRemind_MoreThan2000LettersInMessage_SendAWarningToUser()
        {
            //Arrange
            _message.Text = "MoreThen2000Lettersssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";

            //Act
            await _botControllerServiceAdditionalMethods.AddRemind(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Максимальная длина текста - 2000 символов"));
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Напишите текст напоминания"));
        }

        [Test]
        public async Task AddRemind_LessThan2000LettersInMessage_SetDateMethodCalled()
        {
            //Arrange
            _message.Text = "LessThen2000Letters"; // Менее 2000 символов

            //Act
            await _botControllerServiceAdditionalMethods.AddRemind(_botClient.Object, _message, _user);

            //Verify
            _remindsService.Verify(an => an.AddRemind(It.IsAny<Business.Models.User>(), It.IsAny<string>()));
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Введите дату напоминания в формате: dd.mm.yyyy hh:mm"));
        }
    }
}
