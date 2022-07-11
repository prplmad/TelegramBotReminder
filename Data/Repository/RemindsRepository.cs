using Business.Abstract.Repositories;
using DataStore.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using Business.Models;
using DataStore.Mappers;
using Telegram.Bot;
using Business.Abstract.Wrappers;


namespace DataStore.Repository
{
    public class RemindsRepository : IRemindsRepository
    {
        private readonly ApplicationContext _db;
        private readonly ITelegramBotClientWrapper _TelegramBotClientWrapper;
        public RemindsRepository(ApplicationContext db, ITelegramBotClientWrapper TelegramBotClientWrapper)
        {
            _db = db;
            _TelegramBotClientWrapper = TelegramBotClientWrapper;
        }
        public async Task<bool> AddRemind(Remind remind)
        {
            RemindEntity remindEntity = remind.FromBusinessToEntities();
            var UserId = remind.UserId;
            remindEntity.User = await _db.Users.SingleOrDefaultAsync(x => x.UserId == UserId);
            await _db.AddAsync(remindEntity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyCollection<Remind>> GetReminds(User user)
        {
            await Task.Delay(0);
            UserEntity userEntity = user.FromBusinessToEntities();
            RemindEntity[] reminds = _db.Reminds.Where(p => p.User.UserId == userEntity.UserId).Where(p => p.IsDeleted != true).ToArray();
            List<Remind> listOfReminds = new();
            foreach (var remind in reminds)
            {
                listOfReminds.Add(remind.FromEntitiesToBusiness());
            }
            return listOfReminds;

        }

        public async Task<bool> SetDate(Remind remind)
        {
            RemindEntity remindEntity = remind.FromBusinessToEntities();
            var UserId = remind.UserId;
            try
            {
                var _remind = _db.Reminds.Where(x => x.User.UserId == UserId).OrderByDescending(p => p.CreatedAt).First();
                _remind.RemindDate = remind.RemindDate;
                _remind.IsInvoked = remind.IsInvoked;
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return true;
        }

        public async Task<bool> DeleteRemind(Remind remind)
        {
            RemindEntity remindEntity = remind.FromBusinessToEntities();
            RemindEntity _remind = await _db.Reminds.FindAsync(remindEntity.Id);
            if (_remind != null && _remind.IsDeleted is not true)
            {
                _remind.IsDeleted = true;
                _db.Reminds.Update(_remind);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task SendRemind(ITelegramBotClient botClient)
        {
            {
                var user = _db.Users.Include(s => s.Reminds);
                foreach (var it in user)
                {
                    var defaultDate = new DateTime(0001, 01, 01, 00, 00, 0); // устанавливается до установки даты напоминания методом SetDate
                    foreach (var remind in it.Reminds)
                        if (remind.RemindDate <= DateTime.Now && !remind.IsInvoked && !remind.IsDeleted && remind.RemindDate.Date != defaultDate)
                        {
                            await _TelegramBotClientWrapper.SendTextMessageAsync(botClient, it.UserId, remind.Text);
                            remind.IsInvoked = true;
                            remind.IsDeleted = true;
                        }

                }
                await _db.SaveChangesAsync();
            }
        }
    }
}
