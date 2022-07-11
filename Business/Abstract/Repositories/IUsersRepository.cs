using Business.Models;
using System.Threading.Tasks;


namespace Business.Abstract.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> AddUser(User user);
        Task<State> GetState(User user);
        Task<bool> DoesUserExist(User user);
    }
}
