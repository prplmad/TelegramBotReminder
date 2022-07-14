using Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Telegram.Bot;

namespace Business.Abstract.Services
{
    public interface IRemindsService
    {
        Task<bool> AddRemindAsync(User user, string text);
        Task<bool> SetDateAsync(User user, DateTime date);
        Task<bool> DeleteRemindAsync(User user, int noteid);
        Task<IReadOnlyCollection<Remind>> GetRemindsAsync(User user);
        Task SendRemindAsync(ITelegramBotClient botClient);
    }
}
