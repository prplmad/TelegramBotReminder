using Business.Models;
using System.Threading.Tasks;


namespace Business.Abstract.Repositories
{
    public interface IStatesRepository
    {
        Task<bool> AddState(User user);
        Task<bool> UpdateState(User user);
    }
}
