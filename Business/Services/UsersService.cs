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

        public async Task<State> GetState(User user)
        {
            State state;
            return state = await _usersRepository.GetState(user);
        }

        public async Task<bool> DoesUserExist(User user)
        {
            bool result;
            return result = await _usersRepository.DoesUserExist(user);
        }

        public async Task AddUser(User user)
        {
            await _usersRepository.AddUser(user);
        }

    }
}
