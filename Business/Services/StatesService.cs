using Business.Abstract.Repositories;
using Business.Abstract.Services;
using System.Threading.Tasks;
using Business.Models;

namespace Business
{
    public class StatesService : IStatesService
    {

        private readonly IStatesRepository _statesRepository;
        public StatesService(IStatesRepository statesRepository)
        {
            _statesRepository = statesRepository;
        }

        public async Task<bool> UpdateStateAsync(User user)
        {
            await _statesRepository.UpdateStateAsync(user);
            return true;
        }
    }
}
