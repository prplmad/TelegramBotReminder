using Telegram.Bot;
using Telegram.Bot.Types;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Business.Abstract.Services;
using Business.Abstract.Wrappers;
using Business.Services;
using Business.Models;
using System;
using System.Collections.Generic;

namespace Tests.ChooseStateAdditionalMethodsTests
{
    [TestFixture]
    public class GetRemindsTests
    {
        private ChooseStateAdditionalMethods _chooseStateAdditionalMethods;
        private Mock<ITelegramBotClientWrapper> _telegramBotClientWrapper;
        private Mock<INotesService> _notesService;
        private Mock<IRemindsService> _remindsService;
        private Mock<IStatesService> _statesService;
        private Mock<TelegramBotClient> _botClient;
        private Business.Models.User _user;
        private Message _message;
        private IReadOnlyCollection<Remind> _emptyList = Array.Empty<Remind>();
        private IReadOnlyCollection<Remind> _notAnEmptyList = new List<Remind>() { new Remind() };


        [SetUp]
        public void Init()
        {
            _botClient = new Mock<TelegramBotClient>("SomeToken", null, default);
            _telegramBotClientWrapper = new Mock<ITelegramBotClientWrapper>();
            _notesService = new Mock<INotesService>();
            _remindsService = new Mock<IRemindsService>();
            _statesService = new Mock<IStatesService>();
            _user = new Business.Models.User();
            _message = new Message();
            _chooseStateAdditionalMethods = new ChooseStateAdditionalMethods(_notesService.Object, _remindsService.Object, _statesService.Object, _telegramBotClientWrapper.Object);
        }

        [Test]
        public async Task GetNotes_NotesServiceReturnedAnEmptyList()
        {
            //Arrange
            _remindsService.Setup(gn => gn.GetReminds(_user)).ReturnsAsync(_emptyList);

            //Act
            await _chooseStateAdditionalMethods.GetReminds(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stma => stma.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Напоминания отсутствуют"));
        }

        [Test]
        public async Task GetNotes_NotesServiceReturnedNotAnEmptyList()
        {
            //Arrange
            _remindsService.Setup(gn => gn.GetReminds(_user)).ReturnsAsync(_notAnEmptyList);

            //Act
            await _chooseStateAdditionalMethods.GetReminds(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stma => stma.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Напоминания отсутствуют"), Times.Never());
            _telegramBotClientWrapper.Verify(stma => stma.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), It.IsAny<string>()));
        }
    }
}
