using Business.Models;
using System.Threading.Tasks;


namespace Business.Abstract.Repositories
{
    public interface IStatesRepository
    {
        Task<bool> AddStateAsync(User user);
        Task<bool> UpdateStateAsync(User user);
    }
}
