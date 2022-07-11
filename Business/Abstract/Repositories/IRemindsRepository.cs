using Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot;


namespace Business.Abstract.Repositories
{
    public interface IRemindsRepository
    {
        Task<bool> AddRemind(Remind remind);
        Task<bool> SetDate(Remind remind);
        Task<bool> DeleteRemind(Remind remind);
        Task<IReadOnlyCollection<Remind>> GetReminds(User user);
        Task SendRemind(ITelegramBotClient botClient);
    }
}
