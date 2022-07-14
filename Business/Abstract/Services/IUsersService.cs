using Business.Models;
using System.Threading.Tasks;

namespace Business.Abstract.Services
{
    public interface IUsersService
    {
        Task<State> GetStateAsync(User user);
        Task<bool> DoesUserExistAsync(User user);
        Task AddUserAsync(User user);
    }
}
