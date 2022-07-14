using Business.Abstract.Services;
using System;
using Telegram.Bot;
using Business.Models;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System.Text.RegularExpressions;
using Business.Abstract.Wrappers;

namespace Business.Services
{
    public class BotControllerServiceAdditionalMethods : IBotControllerServiceAdditionalMethods
    {
        private readonly INotesService _notesService;
        private readonly IRemindsService _remindsService;
        private readonly IStatesService _statesService;
        private readonly ITelegramBotClientWrapper _telegramBotClientWrapper;
        private readonly IChooseStateAdditionalMethods _chooseStateAdditionalMethods;
        private const string ADDNOTE = "/addnote",
            DELETENOTE = "/deletenote",
            ADDREMIND = "/addremind",
            DELETEREMIND = "/deleteremind",
            INFO = "/info",
            START = "/start",
            GETNOTES = "/getnotes",
            GETREMINDS = "/getreminds";
        public BotControllerServiceAdditionalMethods(INotesService notesService, IRemindsService remindsService, IStatesService statesService, ITelegramBotClientWrapper TelegramBotClientWrapper, IChooseStateAdditionalMethods chooseStateAdditionalMethods)
        {
            _notesService = notesService;
            _remindsService = remindsService;
            _statesService = statesService;
            _telegramBotClientWrapper = TelegramBotClientWrapper;
            _chooseStateAdditionalMethods = chooseStateAdditionalMethods;
        }
        public async Task<bool> ChooseStateAsync(ITelegramBotClient botClient, Message message, Business.Models.User user)
        {

            State st;
            try
            {
                if (message.Text == null)
                    return true;
                switch (message.Text.ToLower().Trim())
                {
                    case ADDNOTE:
                        st = State.Note;
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напишите текст заметки");
                        break;
                    case DELETENOTE:
                        st = State.DeleteNote;
                        await _chooseStateAdditionalMethods.GetNotesAsync(botClient, message, user);
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Введите Id заметки, которую необходимо удалить");
                        break;
                    case ADDREMIND:
                        st = State.Remind;
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напишите текст напоминания");
                        break;
                    case DELETEREMIND:
                        st = State.DeleteRemind;
                        await _chooseStateAdditionalMethods.GetRemindsAsync(botClient, message, user);
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Введите Id напоминания, которое необходимо удалить");
                        break;
                    case INFO:
                        st = State.None;
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat,
                            "Команды:\n/addnote - создать заметку\n/addremind - создать напоминание\n/getnotes - вывести все заметки\n/getreminds - вывести все запланированные напоминания\n/deletenote - удалить заметку\n/deleteremind - удалить напоминание");
                        break;
                    case START:
                        st = State.None;
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat,
                            "Привет!\nДавай начнём работать.\nЧтобы получить список доступных команд - введите /info");
                        break;
                    case GETNOTES:
                        st = State.None;
                        await _chooseStateAdditionalMethods.GetNotesAsync(botClient, message, user);
                        break;
                    case GETREMINDS:
                        st = State.None;
                        await _chooseStateAdditionalMethods.GetRemindsAsync(botClient, message, user);
                        break;
                    default:
                        st = State.None;
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Я не понимаю этой команды!\nПомощь /info");
                        break;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return true;
            }

            try
            {
                var businessLayerUser = user;
                businessLayerUser.State = st;
                await _statesService.UpdateStateAsync(businessLayerUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return true;
        }
        public async Task AddNoteAsync(ITelegramBotClient botClient, Message message, Models.User user)
        {
            if (message.Text.Length > 2000)
            {
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Максимальная длина текста - 2000 символов. Попробуйте сократить текст и написать еще раз");
            }
            else
            {
                await _notesService.AddNoteAsync(user, message.Text);
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Заметка создана");
            }
        }
        public async Task DeleteNoteAsync(ITelegramBotClient botClient, Message message, Models.User user)
        {
            try
            {
                bool result = int.TryParse(message.Text, out int id);
                if (result == true)
                {
                    if (await _notesService.DeleteNoteAsync(user, id) == true)
                    {
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Заметка удалена");
                    }
                    else
                    {
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Заметки с таким Id не существует");
                    }
                }
                else
                {
                    await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Заметки с таким Id не существует");
                }
            }
            catch (Exception)
            {
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Заметки с таким Id не существует");
            }
        }

        public async Task AddRemindAsync(ITelegramBotClient botClient, Message message, Models.User user)
        {
            if (message.Text.Length > 2000)
            {
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Максимальная длина текста - 2000 символов");
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напишите текст напоминания");
            }
            else
            {
                await _remindsService.AddRemindAsync(user, message.Text);
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Введите дату напоминания в формате: dd.mm.yyyy hh:mm");
            }
        }

        public async Task DeleteRemindAsync(ITelegramBotClient botClient, Message message, Models.User user)
        {
            try
            {
                bool result = int.TryParse(message.Text, out int id);
                if (result)
                {
                    if (await _remindsService.DeleteRemindAsync(user, id) == true)
                    {
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напоминание удалено");
                    }
                    else
                    {
                        await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напоминания с таким Id не существует");
                    }
                }
                else
                {
                    await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напоминания с таким Id не существует");
                }
            }
            catch (Exception)
            {
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напоминания с таким Id не существует");
            }
        }
        public async Task<bool> SetDateAsync(ITelegramBotClient botClient, Message message, Models.User user)
        {
            Regex regex = new(@"\d{2}\W\d{2}\W\d{4}\s\d{2}:\d{2}");
            MatchCollection matches = regex.Matches(message.Text);
            _ = message.Text;

            if (matches.Count == 0 || !DateTime.TryParse(message.Text, out DateTime date))
            {
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Некорректный формат даты, введите заново!\nФормат даты: dd.mm.yyyy hh:mm");
                return true;
            }
            if (date < DateTime.Now)
            {
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Дата не может быть в прошлом, введите заново!\nФормат даты: dd.mm.yyyy hh:mm");
                return true;
            }
            else
            {
                await _remindsService.SetDateAsync(user, date);
                await _telegramBotClientWrapper.SendTextMessageAsync(botClient, message.Chat, "Напоминание успешно создано");
                return true;
            }
        }
    }
}
