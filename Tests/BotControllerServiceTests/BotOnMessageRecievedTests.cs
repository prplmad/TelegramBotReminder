using Telegram.Bot;
using Telegram.Bot.Types;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Business.Abstract.Services;
using Business.Abstract.Wrappers;
using Business.Services;
using Business.Models;
using Microsoft.Extensions.Logging;

namespace Tests.BotControllerServiceTests
{
    [TestFixture]
    public class BotOnMessageRecievedTests
    {
        private Mock<IUsersService> _usersService;
        private Mock<IBotControllerServiceAdditionalMethods> _botControllerServiceAdditionalMethods;
        private BotControllerService _botControllerService;
        private Mock<TelegramBotClient> _botClient;
        private Business.Models.User _user;
        private Message _message;
        private Mock<IMessageWrapper> _messageWrapper;
        private Mock<ILogger<BotControllerService>> _logger;

        [SetUp]
        public void Init()
        {
            _logger = new Mock<ILogger<BotControllerService>>();
            _botClient = new Mock<TelegramBotClient>("SomeToken", null, default);
            _usersService = new Mock<IUsersService>();
            _messageWrapper = new Mock<IMessageWrapper>();
            _botControllerServiceAdditionalMethods = new Mock<IBotControllerServiceAdditionalMethods>();
            _botControllerService = new BotControllerService(_usersService.Object, _botControllerServiceAdditionalMethods.Object, _messageWrapper.Object, _logger.Object);
            _user = new Business.Models.User();
            _message = new Message();
        }

        [Test]
        public async Task BotOnMessageRecieved_StateNone()
        {
            //Arrange
            _messageWrapper.Setup(m => m.FromBot(_botClient.Object, _message)).ReturnsAsync(false);
            _usersService.Setup(gs => gs.GetState(It.IsAny<Business.Models.User>())).ReturnsAsync(State.None);
            _usersService.Setup(due => due.DoesUserExist(It.IsAny<Business.Models.User>())).ReturnsAsync(true);

            //Act
            await _botControllerService.BotOnMessageReceived(_botClient.Object, _message, _user);

            //Verify
            _botControllerServiceAdditionalMethods.Verify(cs => cs.ChooseState(_botClient.Object, _message, _user));
        }

        [Test]
        public async Task BotOnMessageRecieved_StateNote()
        {
            //Arrange
            _usersService.Setup(due => due.DoesUserExist(It.IsAny<Business.Models.User>())).ReturnsAsync(true);
            _usersService.Setup(gs => gs.GetState(It.IsAny<Business.Models.User>())).ReturnsAsync(State.Note);

            //Act
            await _botControllerService.BotOnMessageReceived(_botClient.Object, _message, _user);

            //Verify
            _botControllerServiceAdditionalMethods.Verify(an => an.AddNote(_botClient.Object, _message, _user));
        }

        [Test]
        public async Task BotOnMessageRecieved_StateDeleteNote()
        {
            //Arrange
            _usersService.Setup(due => due.DoesUserExist(It.IsAny<Business.Models.User>())).ReturnsAsync(true);
            _usersService.Setup(gs => gs.GetState(It.IsAny<Business.Models.User>())).ReturnsAsync(State.DeleteNote);

            //Act
            await _botControllerService.BotOnMessageReceived(_botClient.Object, _message, _user);

            //Verify
            _botControllerServiceAdditionalMethods.Verify(dn => dn.DeleteNote(_botClient.Object, _message, _user));
        }

        [Test]
        public async Task BotOnMessageRecieved_AddRemind()
        {
            //Arrange
            _usersService.Setup(due => due.DoesUserExist(It.IsAny<Business.Models.User>())).ReturnsAsync(true);
            _usersService.Setup(gs => gs.GetState(It.IsAny<Business.Models.User>())).ReturnsAsync(State.Remind);

            //Act
            await _botControllerService.BotOnMessageReceived(_botClient.Object, _message, _user);

            //Verify
            _botControllerServiceAdditionalMethods.Verify(ar => ar.AddRemind(_botClient.Object, _message, _user));
        }

        [Test]
        public async Task BotOnMessageRecieved_StateDeleteRemind()
        {
            //Arrange
            _usersService.Setup(due => due.DoesUserExist(It.IsAny<Business.Models.User>())).ReturnsAsync(true);
            _usersService.Setup(gs => gs.GetState(It.IsAny<Business.Models.User>())).ReturnsAsync(State.DeleteRemind);

            //Act
            await _botControllerService.BotOnMessageReceived(_botClient.Object, _message, _user);

            //Verify
            _botControllerServiceAdditionalMethods.Verify(dr => dr.DeleteRemind(_botClient.Object, _message, _user));
        }

        [Test]
        public async Task BotOnMessageRecieved_StateSetDate()
        {
            //Arrange
            _usersService.Setup(due => due.DoesUserExist(It.IsAny<Business.Models.User>())).ReturnsAsync(true);
            _usersService.Setup(gs => gs.GetState(It.IsAny<Business.Models.User>())).ReturnsAsync(State.SetDate);

            //Act
            await _botControllerService.BotOnMessageReceived(_botClient.Object, _message, _user);

            //Verify
            _botControllerServiceAdditionalMethods.Verify(sd => sd.SetDate(_botClient.Object, _message, _user));
        }
    }
}
