using Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot;


namespace Business.Abstract.Repositories
{
    public interface IRemindsRepository
    {
        Task<bool> AddRemindAsync(Remind remind);
        Task<bool> SetDateAsync(Remind remind);
        Task<bool> DeleteRemindAsync(Remind remind);
        Task<IReadOnlyCollection<Remind>> GetRemindsAsync(User user);
        Task SendRemindAsync(ITelegramBotClient botClient);
    }
}
