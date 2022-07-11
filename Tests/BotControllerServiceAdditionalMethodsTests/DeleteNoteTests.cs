using Telegram.Bot;
using Telegram.Bot.Types;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Business.Abstract.Services;
using Business.Abstract.Wrappers;
using Business.Services;
using Business.Models;


namespace Tests.BotControllerServiceAdditionalMethodsTests
{
    [TestFixture]
    public class DeleteNoteTests
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
        public async Task DeleteNote_TextInsteadOfId()
        {
            //Arrange
            _message.Text = "TextInsteadOfId"; // Текст вместо числа

            //Act
            await _botControllerServiceAdditionalMethods.DeleteNote(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Заметки с таким Id не существует"));
        }

        [Test]
        public async Task DeleteNote_CorrectId()
        {
            //Arrange
            _notesService.Setup(gs => gs.DeleteNote(It.IsAny<Business.Models.User>(), It.IsAny<int>())).ReturnsAsync(true);
            _message.Text = "7"; // Корректный Id

            //Act
            await _botControllerServiceAdditionalMethods.DeleteNote(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Заметка удалена"));
        }

        [Test]
        public async Task DeleteNote_NotesServiceDeleteNoteMethodReturnsFalse()
        {
            //Arrange
            _notesService.Setup(gs => gs.DeleteNote(It.IsAny<Business.Models.User>(), It.IsAny<int>())).ReturnsAsync(false);
            _message.Text = "7"; // Корректный Id

            //Act
            await _botControllerServiceAdditionalMethods.DeleteNote(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Заметки с таким Id не существует"));
        }
    }
}
