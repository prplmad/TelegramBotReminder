using Business.Abstract.Repositories;
using DataStore.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Business.Models;

namespace DataStore.Repository
{
    public class StatesRepository : IStatesRepository
    {
        private readonly ApplicationContext _db;
        public StatesRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<bool> UpdateStateAsync(User user)
        {
            StateEntity state = await _db.States.SingleOrDefaultAsync(x => x.UserId == user.Id);
            state.TelegramState = (TelegramState)user.State;
            state.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddStateAsync(User user)
        {
            StateEntity stateEntity = new();
            stateEntity.UpdatedAt = DateTime.Now;
            stateEntity.UserId = user.Id;
            await _db.AddAsync(stateEntity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
