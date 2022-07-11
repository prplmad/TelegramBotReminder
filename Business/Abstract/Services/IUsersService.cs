using Business.Models;
using System.Threading.Tasks;

namespace Business.Abstract.Services
{
    public interface IUsersService
    {
        Task<State> GetState(User user);
        Task<bool> DoesUserExist(User user);
        Task AddUser(User user);
    }
}
