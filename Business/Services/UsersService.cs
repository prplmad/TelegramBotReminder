using System;
using Business.Abstract.Services;
using Business.Abstract.Repositories;
using System.Threading.Tasks;
using Business.Models;

namespace Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<State> GetStateAsync(User user)
        {
            State state;
            return state = await _usersRepository.GetStateAsync(user);
        }

        public async Task<bool> DoesUserExistAsync(User user)
        {
            bool result;
            return result = await _usersRepository.DoesUserExistAsync(user);
        }

        public async Task AddUserAsync(User user)
        {
            await _usersRepository.AddUserAsync(user);
        }

    }
}
