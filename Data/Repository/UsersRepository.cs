using Business.Abstract.Repositories;
using DataStore.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataStore.Mappers;
using Business.Models;

namespace DataStore.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationContext _db;
        private readonly IStatesRepository _statesRepository;
        public UsersRepository(ApplicationContext db, IStatesRepository statesRepository)
        {
            _db = db;
            _statesRepository = statesRepository;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            UserEntity userEntity = user.FromBusinessToEntities();
            await _db.AddAsync(userEntity);  // добавляем пользователя в таблицу Users
            await _db.SaveChangesAsync();
            await _statesRepository.AddStateAsync(user); //добавляем запись о состоянии пользователя в таблицу States
            return true;
        }

        public async Task<State> GetStateAsync(User user)
        {
            var result = await _db.Users.SingleOrDefaultAsync(x => x.UserId == user.Id);
            StateEntity state = await _db.States.SingleOrDefaultAsync(x => x.UserId == user.Id);
            return (State)state.TelegramState;
        }

        public async Task<bool> DoesUserExistAsync(User user)
        {
            var result = await _db.Users.SingleOrDefaultAsync(x => x.UserId == user.Id);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
