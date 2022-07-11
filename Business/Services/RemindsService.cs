using System;
using Business.Abstract.Services;
using Business.Abstract.Repositories;
using System.Threading.Tasks;
using Business.Models;
using System.Collections.Generic;
using Telegram.Bot;


namespace Services
{
    public class RemindsService : IRemindsService
    {

        private readonly IRemindsRepository _remindsRepository;
        private readonly IStatesRepository _statesRepository;

        public RemindsService(IRemindsRepository remindsRepository, IUsersRepository usersRepository, IStatesRepository statesRepository)
        {
            _remindsRepository = remindsRepository;
            _statesRepository = statesRepository;
        }
        public async Task<bool> AddRemind(User user, string text)
        {
            Remind remind = new();
            remind.Text = text;
            remind.CreatedAt = DateTime.Now;
            remind.UserId = user.Id;
            await _remindsRepository.AddRemind(remind);
            user.State = State.SetDate;
            await _statesRepository.UpdateState(user);
            return true;
        }
        public async Task<bool> SetDate(User user, DateTime date)
        {
            Remind remind = new();
            remind.RemindDate = date;
            remind.UserId = user.Id;
            await _remindsRepository.SetDate(remind);
            user.State = State.None;
            await _statesRepository.UpdateState(user);
            return true;
        }

        public async Task<bool> DeleteRemind(User user, int remindid)
        {
            Remind remind = new();
            remind.Id = remindid;
            if (await _remindsRepository.DeleteRemind(remind))
            {
                user.State = State.None;
                await _statesRepository.UpdateState(user);
                return true;
            }
            else
            {
                user.State = State.None;
                await _statesRepository.UpdateState(user);
                return false;
            }
        }

        public async Task<IReadOnlyCollection<Remind>> GetReminds(User user)
        {
            var listOfReminds = await _remindsRepository.GetReminds(user);
            return listOfReminds;
        }

        public async Task SendRemind(ITelegramBotClient botClient)
        {
            await _remindsRepository.SendRemind(botClient);
        }

    }
}
