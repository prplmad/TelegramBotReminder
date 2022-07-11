using Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Telegram.Bot;

namespace Business.Abstract.Services
{
    public interface IRemindsService
    {
        Task<bool> AddRemind(User user, string text);
        Task<bool> SetDate(User user, DateTime date);
        Task<bool> DeleteRemind(User user, int noteid);
        Task<IReadOnlyCollection<Remind>> GetReminds(User user);
        Task SendRemind(ITelegramBotClient botClient);
    }
}
