using Business.Abstract.Services;
using Telegram.Bot;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Business.Abstract.Wrappers;

namespace Business.Services
{
    public class ChooseStateAdditionalMethods : IChooseStateAdditionalMethods
    {
        private readonly INotesService _notesService;
        private readonly IRemindsService _remindsService;
        private readonly ITelegramBotClientWrapper _telegramBotClientWrapper;
        public ChooseStateAdditionalMethods(INotesService notesService, IRemindsService remindsService, IStatesService statesService, ITelegramBotClientWrapper TelegramBotClientWrapper)
        {
            _notesService = notesService;
            _remindsService = remindsService;
            _telegramBotClientWrapper = TelegramBotClientWrapper;
        }

        public async Task GetNotes(ITelegramBotClient botClient, Message message, Business.Models.User user)
        {
            int noteCount = 0;
            var notesList = await _notesService.GetNotes(user);
            if (notesList.Count == 0)
            {
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Заметки отсутствуют");
            }
            else
            {
                foreach (var note in notesList)
                {
                    await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, $"Заметка {++noteCount}:\nТекст: {note.Text}\nДата создания: {note.CreatedAt:dd:MM:yyyy HH:mm}\nId для удаления: {note.Id}");
                }
            }

        }
        public async Task GetReminds(ITelegramBotClient botClient, Message message, Business.Models.User user)
        {
            int remindCount = 0;
            var remindsList = await _remindsService.GetReminds(user);
            if (remindsList.Count == 0)
            {
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напоминания отсутствуют");
            }
            else
            {
                foreach (var remind in remindsList)
                {
                    await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, $"Напоминание {++remindCount}:\nТекст: {remind.Text}\nДата создания: {remind.CreatedAt:dd:MM:yyyy HH:mm}\nДата напоминания: {remind.RemindDate:dd:MM:yyyy HH:mm}\nId для удаления: {remind.Id}");
                }
            }

        }

    }
}
