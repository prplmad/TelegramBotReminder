using Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Business.Abstract.Services
{
    public interface IStatesService
    {
        Task<bool> UpdateStateAsync(User user);
    }
}
