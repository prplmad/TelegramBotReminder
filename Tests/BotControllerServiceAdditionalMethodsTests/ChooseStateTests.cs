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

namespace Tests.BotControllerServiceAdditionalMethodsTests
{
    [TestFixture]
    public class ChooseStateTests
    {
        private Mock<IChooseStateAdditionalMethods> _chooseStateAdditionalMethods;
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
            _chooseStateAdditionalMethods = new Mock<IChooseStateAdditionalMethods>();
            _botClient = new Mock<TelegramBotClient>("SomeToken", null, default);
            _notesService = new Mock<INotesService>();
            _statesService = new Mock<IStatesService>();
            _remindsService = new Mock<IRemindsService>();
            _telegramBotClientWrapper = new Mock<ITelegramBotClientWrapper>();
            _botControllerServiceAdditionalMethods = new BotControllerServiceAdditionalMethods(_notesService.Object, _remindsService.Object, _statesService.Object, _telegramBotClientWrapper.Object, _chooseStateAdditionalMethods.Object);
            _user = new Business.Models.User();
            _message = new Message();
        }

        [Test]
        public async Task ChooseState_NotACommand()
        {
            //Arrange
            _message.Text = "SomeText";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Я не понимаю этой команды!\nПомощь /info"));
        }

        [Test]
        public async Task ChooseState_AddNote()
        {
            //Arrange
            _message.Text = "/addnote";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Напишите текст заметки"));
        }

        [Test]
        public async Task ChooseState_DeleteNote()
        {
            //Arrange
            _message.Text = "/deletenote";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _chooseStateAdditionalMethods.Verify(gn => gn.GetNotesAsync(_botClient.Object, _message, _user));
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Введите Id заметки, которую необходимо удалить"));

        }

        [Test]
        public async Task ChooseState_AddRemind()
        {
            //Arrange
            _message.Text = "/addremind";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Напишите текст напоминания"));
        }

        [Test]
        public async Task ChooseState_DeleteRemind()
        {
            //Arrange
            _message.Text = "/deleteremind";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _chooseStateAdditionalMethods.Verify(gn => gn.GetRemindsAsync(_botClient.Object, _message, _user));
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Введите Id напоминания, которое необходимо удалить"));
        }


        [Test]
        public async Task ChooseState_Info()
        {
            //Arrange
            _message.Text = "/info";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Команды:\n/addnote - создать заметку\n/addremind - создать напоминание\n/getnotes - вывести все заметки\n/getreminds - вывести все запланированные напоминания\n/deletenote - удалить заметку\n/deleteremind - удалить напоминание"));
        }

        [Test]
        public async Task ChooseState_Start()
        {
            //Arrange
            _message.Text = "/start";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _telegramBotClientWrapper.Verify(stm => stm.SendTextMessageAsync(_botClient.Object, It.IsAny<ChatId>(), "Привет!\nДавай начнём работать.\nЧтобы получить список доступных команд - введите /info"));
        }

        [Test]
        public async Task ChooseState_GetNotes()
        {
            //Arrange
            _message.Text = "/getnotes";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _chooseStateAdditionalMethods.Verify(gn => gn.GetNotesAsync(_botClient.Object, _message, _user));
        }

        [Test]
        public async Task ChooseState_GetReminds()
        {
            //Arrange
            _message.Text = "/getreminds";

            //Act
            await _botControllerServiceAdditionalMethods.ChooseStateAsync(_botClient.Object, _message, _user);

            //Verify
            _chooseStateAdditionalMethods.Verify(gn => gn.GetRemindsAsync(_botClient.Object, _message, _user));
        }
    }
}
