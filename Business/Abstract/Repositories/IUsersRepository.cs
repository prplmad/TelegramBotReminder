using Business.Models;
using System.Threading.Tasks;


namespace Business.Abstract.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<State> GetStateAsync(User user);
        Task<bool> DoesUserExistAsync(User user);
    }
}
