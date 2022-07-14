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
    public class AddNoteTests
    {
        private Mock<IChooseStateAdditionalMethods> _chooseStateAdditionalMethods;
        private Mock<INotesService> _notesService;
        private Mock<IRemindsService> _remindsService;
        private Mock<IStatesService> _statesService;
        private Mock<ITelegramBotClientWrapper> _TelegramBotClientWrapper;
        private BotControllerServiceAdditionalMethods _botControllerServiceAdditionalMethods;
        private Mock<TelegramBotClient> _botClient;
        private Business.Models.User _user;
        private Message _message;


        [SetUp]
        public void Init()
        {
            _botClient = new Mock<TelegramBotClient>("SomeToken", null, default);
            _notesService = new Mock<INotesService>();
            _statesService = new Mock<IStatesService>();
            _remindsService = new Mock<IRemindsService>();
            _chooseStateAdditionalMethods = new Mock<IChooseStateAdditionalMethods>();
            _TelegramBotClientWrapper = new Mock<ITelegramBotClientWrapper>();
            _botControllerServiceAdditionalMethods = new BotControllerServiceAdditionalMethods(_notesService.Object, _remindsService.Object, _statesService.Object, _TelegramBotClientWrapper.Object, _chooseStateAdditionalMethods.Object);
            _user = new Business.Models.User();
            _message = new Message();
        }

        [Test]
        public async Task AddNote_MoreThan2000LettersInMessage_SendAWarningToUser()
        {
            //Arrange
            _message.Text = "MoreThen2000Lettersssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";

            //Act 
            await _botControllerServiceAdditionalMethods.AddNoteAsync(_botClient.Object, _message, _user);

            //Verify
            _TelegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Максимальная длина текста - 2000 символов. Попробуйте сократить текст и написать еще раз"));
        }

        [Test]
        public async Task AddNote_LessThan2000LettersInMessage_NoteCreated()
        {
            //Arrange
            _message.Text = "LessThen2000Letters";

            //Act
            await _botControllerServiceAdditionalMethods.AddNoteAsync(_botClient.Object, _message, _user);

            //Verify
            _notesService.Verify(an => an.AddNoteAsync(It.IsAny<Business.Models.User>(), It.IsAny<string>()));
            _TelegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Заметка создана"));
        }
    }
}
